using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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
        public RaspberryPi Get(int id)
        {
            return getPisFromDB("Select * from RaspberryPis where id=@id", ("@id", id))[0];
        }

        // POST api/<RaspberyPisController>
        [HttpPost]
        public IActionResult Post([FromBody] RaspberryPi value)
        {
            string insertUserSql =
                "insert into RaspberryPis (location, isActive, profileID) values (@location, @isActive, @profileID)";
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertUserSql, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@location", value.Location);
                    insertCommand.Parameters.AddWithValue("@isActive", value.IsActive);
                    insertCommand.Parameters.AddWithValue("@profileID", value.ProfileID);
                    insertCommand.ExecuteNonQuery();
                    return CreatedAtAction("Get", new { id = value.ID }, value);
                }
            }
        }

        // PUT api/<RaspberyPisController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RaspberryPi value)
        {
            if (id != value.ID)
                return BadRequest();
            try
            {
                Get(id);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            string updatePiSql =
                "update RaspberryPis set location=@location, isActive=@isActive, profileID=@profileID where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(updatePiSql, ("@location", value.Location), ("@isActive", value.IsActive), ("@profileID", value.ProfileID), ("@id", value.ID));
            return Ok();
        }

        // DELETE api/<RaspberyPisController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Get(id);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            string deletePiSql = "Delete from RaspberryPis where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(deletePiSql, ("@id", id));
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

                            pis.Add(temp);
                        }
                    }
                }
            }

            return pis;
        }
    }
}
