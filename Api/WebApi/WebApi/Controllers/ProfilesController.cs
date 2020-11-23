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
        public Profile Get(int id)
        {
            return getProfilesFromDB("Select * from Profiles where id=@id", ("@id", id))[0];
        }

        // POST api/<ProfilesController>
        [HttpPost]
        public IActionResult Post([FromBody] Profile value)
        {
            string insertUserSql =
                "insert into Profiles (companyName, city, joinDate, phone, address, country) values (@companyName, @city, @joinDate, @phone, @address, @country)";
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertUserSql, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@companyName", value.companyName);
                    insertCommand.Parameters.AddWithValue("@city", value.city);
                    insertCommand.Parameters.AddWithValue("@joinDate", value.joinDate);
                    insertCommand.Parameters.AddWithValue("@phone", value.phone);
                    insertCommand.Parameters.AddWithValue("@address", value.address);
                    insertCommand.Parameters.AddWithValue("@country", value.country);
                    insertCommand.ExecuteNonQuery();
                    return CreatedAtAction("Get", new {id = value.ID}, value);
                }
            }
        }

        // PUT api/<ProfilesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Profile value)
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
            string updateUserSql =
                "update Profiles set companyName=@companyName, city=@city, joinDate=@joinDate, phone=@phone, address=@address, country=@country where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(updateUserSql, ("@companyName", value.companyName),
                ("@city", value.city), ("@joinDate", value.joinDate), ("@phone", value.phone),
                ("@address", value.address), ("@country", value.country), ("@id", id));
            return Ok();
        }

        // DELETE api/<ProfilesController>/5
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
            string deleteUserSql = "Delete from Profiles where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(deleteUserSql, ("@id", id));
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
                            temp.companyName = reader.GetString(1);
                            temp.city = reader.GetString(2);
                            temp.joinDate = reader.GetDateTime(3);
                            temp.phone = reader.GetString(4);
                            temp.address = reader.GetString(5);
                            temp.country = reader.GetString(6);

                            profiles.Add(temp);
                        }
                    }
                }
            }

            return profiles;
        }
    }
}
