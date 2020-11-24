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
    public class ProfilesController : ControllerBase
    {
        // GET: api/<ProfilesController>
        [HttpGet]
        public List<Profile> Get()
        {
            return getProfilesFromDB("Select * from Profiles");
        }

        // GET api/<ProfilesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(getProfilesFromDB("Select * from Profiles where id=@id", ("@id", id))[0]);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // POST api/<ProfilesController>
        [HttpPost]
        public IActionResult Post([FromBody] Profile value)
        {
            string insertProfileSql =
                "insert into Profiles (companyName, city, joinDate, phone, address, country) values (@companyName, @city, @joinDate, @phone, @address, @country)";
            StaticMethods.PostToDB(insertProfileSql, ("@companyName", value.CompanyName), ("@city", value.City), ("@joinDate", value.JoinDate), 
                ("@phone", value.Phone), ("@address", value.Address), ("@country", value.Country));
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/<ProfilesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Profile value)
        {
            if (id != value.ID)
                return BadRequest();
            var getProfile = Get(id);
            if (getProfile.GetType() == typeof(NotFoundResult))
                return NotFound();
            string updateUserSql =
                "update Profiles set companyName=@companyName, city=@city, joinDate=@joinDate, phone=@phone, address=@address, country=@country where id=@id";
            StaticMethods.updateOrDeleteFromDB(updateUserSql, ("@companyName", value.CompanyName),
                ("@city", value.City), ("@joinDate", value.JoinDate), ("@phone", value.Phone),
                ("@address", value.Address), ("@country", value.Country), ("@id", id));
            return Ok();
        }

        // DELETE api/<ProfilesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var getProfile = Get(id);
            if (getProfile.GetType() == typeof(NotFoundResult))
                return NotFound();
            string deleteUserSql = "Delete from Profiles where id=@id";
            StaticMethods.updateOrDeleteFromDB(deleteUserSql, ("@id", id));
            return Ok();
        }

        private List<Profile> getProfilesFromDB(string sqlQuery, params (string, object)[] paramList)
        {
            List<Profile> profiles = new List<Profile>();
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
                            Profile temp = new Profile();
                            temp.ID = reader.GetInt32(0);
                            temp.CompanyName = reader.GetString(1);
                            temp.City = reader.GetString(2);
                            temp.JoinDate = reader.GetDateTime(3);
                            temp.Phone = reader.GetString(4);
                            temp.Address = reader.GetString(5);
                            temp.Country = reader.GetString(6);

                            profiles.Add(temp);
                        }
                    }
                }
            }
            return profiles;
        }
    }
}
