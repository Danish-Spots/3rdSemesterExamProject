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
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return getUsersFromDB("select id, userName, password, email, profileID from Users");
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return getUsersFromDB("select id, userName, password, email, profileID from Users where id = @id", ("@id", id))[0];
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            string insertUserSql =
                "insert into Users (userName, password, email, profileID) values (@userName, @password, @email, @profileID)";
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertUserSql, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@userName", value.userName);
                    insertCommand.Parameters.AddWithValue("@password", value.password);
                    insertCommand.Parameters.AddWithValue("@email", value.email);
                    insertCommand.Parameters.AddWithValue("@profileID", value.profileID);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (id != value.ID)
                return BadRequest();
            string updateUserSql =
                "update Users set userName=@userName, password=@password, email=@email, profile=@profile where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(updateUserSql, ("@userName", value.userName), ("@password", value.password), ("@email", value.email), ("@profileID", value.profileID));
            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string deleteUserSql = "Delete from Users where id=@id";
            StaticMethods.updateOrDeleteUserFromDB(deleteUserSql, ("@id", id));
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
                            temp.userName = reader.GetString(1);
                            temp.password = reader.GetString(2);
                            temp.email = reader.GetString(3);
                            temp.profileID = reader.GetInt32(4);

                            users.Add(temp);
                        }
                    }
                }
            }

            return users;
        }

        
    }
}
