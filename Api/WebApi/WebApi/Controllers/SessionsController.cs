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
    public class SessionsController : ControllerBase
    {
        // GET: api/<SessionsController>
        [HttpGet]
        public List<Session> Get()
        {
            return getSessionsFromDB("Select * from Sessions");
        }

        // GET api/<SessionsController>/5
        [HttpGet("{id}")]
        public Session Get(int id)
        {
            return getSessionsFromDB("Select * from Sessions where id=@id", ("@id", id))[0];
        }

        // POST api/<SessionsController>
        [HttpPost]
        public IActionResult Post([FromBody] Session value)
        {
            try
            {
                Session check = getSessionsFromDB("Select * from Sessions where userID=@userID",
                    ("@userID", value.UserID))[0];

                return Conflict();
            }
            catch (Exception e)
            {
                string insertSessionsSql =
                    "insert into Sessions ([key], userID) values (@key, @userID)";
                using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
                {
                    databaseConnection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSessionsSql, databaseConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@key", value.Key);
                        insertCommand.Parameters.AddWithValue("@userID", value.UserID);
                        insertCommand.ExecuteNonQuery();
                    }
                }

            }

            return Ok(new {message = $"Created new session", userID = value.UserID });
        }

        // PUT api/<SessionsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Session value)
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
            string updateSessionSql =
                "update Sessions set [key]=@key, userID=@userID where id=@id";
            StaticMethods.updateOrDeleteFromDB(updateSessionSql, ("@key", value.Key), ("@userID", value.UserID), ("@id", value.ID));
            return Ok();
        }

        // DELETE api/<SessionsController>/5
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
            string deleteSessionSql = "Delete from Sessions where id=@id";
            StaticMethods.updateOrDeleteFromDB(deleteSessionSql, ("@id", id));
            return Ok();
        }

        private List<Session> getSessionsFromDB(string sqlQuery, params (string, object)[] paramList)
        {
            List<Session> sessions = new List<Session>();

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
                            Session temp = new Session();
                            temp.ID = reader.GetInt32(0);
                            temp.Key = reader.GetString(1);
                            temp.UserID = reader.GetInt32(2);

                            sessions.Add(temp);
                        }
                    }
                }
            }

            return sessions;
        }
    }
}
