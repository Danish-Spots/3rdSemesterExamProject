using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(getSessionsFromDB("Select * from Sessions where id=@id", ("@id", id))[0]);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            
        }

        // GET api/<SessionsController>/getSessionKey={sessionKey}
        [HttpGet("getSessionKey={sessionKey}")]
        public IActionResult GetSK(string sessionKey)
        {
            try
            {
                return Ok(getSessionsFromDB("select * from Sessions where [key]=@key", ("@key", sessionKey))[0]);
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<SessionsController>/sessions/userID
        [HttpGet("sessions/{userID}")]
        public IActionResult GetByUID(int userID)
        {
            try
            {
                return Ok(getSessionsFromDB("Select * from Sessions where userID=@userID",
                    ("@userID", userID))[0]);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // POST api/<SessionsController>
        [HttpPost]
        public IActionResult Post([FromBody] Session value)
        {
            var getSession = GetByUID(value.UserID);
            if (getSession.GetType() == typeof(OkObjectResult))
                return Conflict();
            string insertSessionsSql =
                "insert into Sessions ([key], userID) values (@key, @userID)";
            var postResults= StaticMethods.PostToDB(insertSessionsSql, ("@key", value.Key), ("@userID", value.UserID));
            if (postResults == staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE)
                return BadRequest();
            return CreatedAtAction("GET", new {id=value.ID}, value);
        }

        // PUT api/<SessionsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Session value)
        {
            if (id != value.ID)
                return BadRequest();
            var getSession = Get(value.ID);
            if (getSession.GetType() == typeof(NotFoundResult))
                return NotFound();
            string updateSessionSql =
                "update Sessions set [key]=@key where id=@id";
            StaticMethods.updateOrDeleteFromDB(updateSessionSql, ("@key", value.Key), ("@id", value.ID));
            return Ok();
        }

        // DELETE api/<SessionsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var getSession = Get(id);
            if (getSession.GetType() == typeof(NotFoundResult))
                return NotFound();
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
