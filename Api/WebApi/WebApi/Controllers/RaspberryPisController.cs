using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Static;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaspberryPisController : ControllerBase
    {
        // GET: api/<RaspberyPisController>
        [HttpGet]
        public List<RaspberryPi> Get()
        {
            return getPisFromDB("Select * from RaspberryPis");
        }

        // GET api/<RaspberyPisController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(getPisFromDB("Select * from RaspberryPis where id=@id", ("@id", id))[0]);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("/MostDangerousLocation")]
        public IActionResult MostDangerousLocation()
        {
            string sqlQuery = "Select * from RaspberryPis where [location] = (Select Top 1[location] from RaspberryPis inner join (select RPI_Id, count(RPI_Id) as 'Total' from tests where hasFever = 'true' group by RPI_Id) as query1 on query1.RPI_Id = RaspberryPis.ID order by Total desc)";
            try
            {
                return Ok(getPisFromDB(sqlQuery));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // POST api/<RaspberyPisController>
        [HttpPost]
        public IActionResult Post([FromBody] RaspberryPi value)
        {
            string insertRPISql =
                "insert into RaspberryPis (location, isActive, profileID, latitude, longitude, isAccountConfirmed) output inserted.id values (@location, @isActive, @profileID, @latitude, @longitude, @isAccountConfirmed)";
            var postResults = StaticMethods.PostToDB(insertRPISql, ("@location", value.Location), ("@isActive", value.IsActive), ("@profileID", value.ProfileID), ("@latitude", value.Latitude), ("@longitude", value.Longitude), ("@isAccountConfirmed", value.IsAccountConfirmed));
            if (postResults.Item1 == staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE)
                return BadRequest();
            if (postResults.Item1 == staticData.ERRORS.GENERIC_ERROR)
                return StatusCode(StatusCodes.Status500InternalServerError);
            value.ID = postResults.Item2;
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/<RaspberyPisController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RaspberryPi value)
        {
            if (id != value.ID)
                return BadRequest();
            var getRPI = Get(id);
            if (getRPI.GetType() == typeof(NotFoundResult))
                return NotFound();
            string updatePiSql =
                "update RaspberryPis set location=@location, isActive=@isActive, profileID=@profileID, latitude=@latitude, longitude=@longitude, isAccountConfirmed=@isAccountConfirmed where id=@id";
            StaticMethods.updateOrDeleteFromDB(updatePiSql, ("@location", value.Location), ("@isActive", value.IsActive), ("@profileID", value.ProfileID), ("@latitude", value.Latitude), ("@longitude", value.Longitude), ("@isAccountConfirmed", value.IsAccountConfirmed), ("@id", value.ID));
            return Ok();
        }

        // DELETE api/<RaspberyPisController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var getRPI = Get(id);
            if (getRPI.GetType() == typeof(NotFoundResult))
                return NotFound();
            string deletePiSql = "Delete from RaspberryPis where id=@id";
            StaticMethods.updateOrDeleteFromDB(deletePiSql, ("@id", id));
            return Ok();
        }

        private List<RaspberryPi> getPisFromDB(string sqlQuery, params (string, object)[] paramList)
        {
            List<RaspberryPi> pis = new List<RaspberryPi>();

            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                using (SqlCommand selectCommand = new SqlCommand(sqlQuery, databaseConnection))
                {
                    databaseConnection.Open();
                    foreach (var pTuple in paramList)
                    {
                        selectCommand.Parameters.AddWithValue(pTuple.Item1, pTuple.Item2);
                    }
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RaspberryPi temp = new RaspberryPi();
                            temp.ID = reader.GetInt32(0);
                            temp.Location = reader.GetString(1);
                            temp.IsActive = reader.GetBoolean(2);
                            temp.ProfileID = reader.GetInt32(3);
                            temp.Longitude = reader.GetString(4);
                            temp.Latitude = reader.GetString(5);
                            temp.IsAccountConfirmed = reader.GetBoolean(6);

                            pis.Add(temp);
                        }
                    }
                }
            }
            return pis;
        }
    }
}
