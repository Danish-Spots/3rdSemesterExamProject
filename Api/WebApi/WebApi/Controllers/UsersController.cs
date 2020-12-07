using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        [HttpGet("login/{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                User user = getUsersFromDB(
                    "select id, userName, password, email, profileID from Users where username=@username",
                    ("@username", username))[0];

                if (Argon2.Verify(user.Password, password))
                {
                    SessionsController s = new SessionsController();
                    string sessionKey = StaticMethods.GenerateSessionKey();
                    s.Post(new Session() {Key = sessionKey, UserID = user.ID});
                    Dictionary<string, string> re = new Dictionary<string, string>
                    {
                        {"sessionKey", sessionKey},
                        {"profileID", user.ProfileID.ToString()}
                    };
                    string returnString = JsonConvert.SerializeObject(re);
                    return Ok(returnString);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(getUsersFromDB("select id, userName, password, email, profileID from Users where id=@id", ("@id", id))[0]);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // GET api/<UsersController>/username/testuser
        [HttpGet("username/{userName}")]
        public IActionResult Get(string userName)
        {
            try
            {
                User u = getUsersFromDB("Select * from Users where userName LIKE @userName",
                    ("@userName", userName))[0];
                return Ok(u);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            var getUser = Get(value.UserName);
            value.Password = Argon2.Hash(value.Password);
            if (getUser.GetType() == typeof(OkObjectResult))
                return Conflict();
            string insertUserSql =
                "insert into Users (userName, password, email, profileID) output inserted.id values (@userName, @password, @email, @profileID)";
            var postResults = StaticMethods.PostToDB(insertUserSql, ("@userName", value.UserName), ("@password", value.Password), ("@email", value.Email), ("@profileID", value.ProfileID));
            if (postResults.Item1 == staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE)
                return BadRequest();
            if (postResults.Item1 == staticData.ERRORS.GENERIC_ERROR)
                return StatusCode(StatusCodes.Status500InternalServerError);
            value.ID = postResults.Item2;
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (id != value.ID)
                return BadRequest();
            var getUser = Get(id);
            if (getUser.GetType() == typeof(NotFoundResult))
                return NotFound();
            string updateUserSql =
                "update Users set password=@password, email=@email, profileID=@profileID where id=@id";
            StaticMethods.updateOrDeleteFromDB(updateUserSql,  ("@password", value.Password), ("@email", value.Email), ("@profileID", value.ProfileID), ("@id", value.ID));
            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var getUser = Get(id);
            if (getUser.GetType() == typeof(NotFoundResult))
                return NotFound();
            string deleteUserSql = "Delete from Users where id=@id";
            StaticMethods.updateOrDeleteFromDB(deleteUserSql, ("@id", id));
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
