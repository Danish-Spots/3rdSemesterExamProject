using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApi.Models;
using WebApi.Static;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public List<User> Get()
        {
            return getUsersFromDB("select id, userName, password, email, profileID from Users");
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return getUsersFromDB("select id, userName, password, email, profileID from Users where id=@id", ("@id", id))[0];
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            try
            {
                User check = getUsersFromDB("Select * from Users where userName LIKE @userName",
                    ("@userName", value.UserName))[0];
                
                return Conflict();
            }
            catch (Exception e)
            {
                string insertUserSql =
                    "insert into Users (userName, password, email, profileID) values (@userName, @password, @email, @profileID)";
                using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
                {
                    databaseConnection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertUserSql, databaseConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@userName", value.UserName);
                        insertCommand.Parameters.AddWithValue("@password", value.Password);
                        insertCommand.Parameters.AddWithValue("@email", value.Email);
                        insertCommand.Parameters.AddWithValue("@profileID", value.ProfileID);
                        insertCommand.ExecuteNonQuery();
                    }
                }
               
            }

            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
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
                "update Users set userName=@userName, password=@password, email=@email, profileID=@profileID where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(updateUserSql, ("@userName", value.UserName), ("@password", value.Password), ("@email", value.Email), ("@profileID", value.ProfileID), ("@id", value.ID));
            return Ok();
        }

        // DELETE api/<UsersController>/5
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
            string deleteUserSql = "Delete from Users where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(deleteUserSql, ("@id", id));
            return Ok();
        }

        private List<User> getUsersFromDB(string sqlQuery, params (string, object)[] paramList)
        {
            List<User> users = new List<User>();

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
                            User temp = new User();
                            temp.ID = reader.GetInt32(0);
                            temp.UserName = reader.GetString(1);
                            temp.Password = reader.GetString(2);
                            temp.Email = reader.GetString(3);
                            temp.ProfileID = reader.GetInt32(4);

                            users.Add(temp);
                        }
                    }
                }
            }

            return users;
        }

        
    }
}
