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
    public class TestsController : ControllerBase
    {
        // GET: api/<TestsController>
        [HttpGet]
        public List<Test> Get()
        {
            return getTestsFromDB("Select * from Tests");
        }

        // GET api/<TestsController>/5
        [HttpGet("{id}")]
        public Test Get(int id)
        {
            return getTestsFromDB("Select * from Tests where id=@id", ("@id", id))[0];
        }

        // POST api/<TestsController>
        [HttpPost]
        public IActionResult Post([FromBody] Test value)
        {
            string insertTestSql =
                "insert into Tests (temperature, timeOfDataRecording, RPI_ID, hasFever) values (@temperature, @timeOfDataRecording, @RPI_ID, @hasFever)";
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertTestSql, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@temperature", value.Temperature);
                    insertCommand.Parameters.AddWithValue("@timeOfDataRecording", value.TimeOfDataRecording);
                    insertCommand.Parameters.AddWithValue("@RPI_ID", value.RaspberryPiID);
                    insertCommand.Parameters.AddWithValue("@hasFever", value.HasFever);
                    insertCommand.ExecuteNonQuery();
                    return CreatedAtAction("Get", new { id = value.ID }, value);
                }
            }
        }

        // PUT api/<TestsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Test value)
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
                "update Tests set temperature=@temperature, timeOfDataRecording=@timeOfDataRecording, RPI_ID=@RPI_ID, hasFever=@hasFever where id=@id";
            StaticMethods.updateOrDeleteFromDB(updateUserSql, ("@temperature", value.Temperature), ("@timeOfDataRecording", value.TimeOfDataRecording), ("@RPI_ID", value.RaspberryPiID), ("@hasFever", value.HasFever), ("@id", value.ID));
            return Ok();
        }

        // DELETE api/<TestsController>/5
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
            string deleteUserSql = "Delete from Tests where id=@id";
            StaticMethods.updateOrDeleteFromDB(deleteUserSql, ("@id", id));
            return Ok();
        }

        private List<Test> getTestsFromDB(string sqlQuery, params (string, object)[] paramList)
        {
            List<Test> tests = new List<Test>();

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
                            Test temp = new Test();
                            temp.ID = reader.GetInt32(0);
                            temp.Temperature = reader.GetDouble(1);
                            temp.TimeOfDataRecording = reader.GetDateTime(2);
                            temp.RaspberryPiID = reader.GetInt32(3);
                            temp.HasFever = reader.GetBoolean(4);

                            tests.Add(temp);
                        }
                    }
                }
            }

            return tests;
        }
    }
}
