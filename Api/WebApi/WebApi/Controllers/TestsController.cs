using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(getTestsFromDB("Select * from Tests where id=@id", ("@id", id))[0]);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            
        }

        // GET NoTests: /api/TestCount
        [HttpGet("/TestCount")]
        public IActionResult TestCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM TESTS";

            try
            {
               return Ok(getCount(sqlQuery));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/FeverCount")]
        public IActionResult FeverCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE [hasFever]='True'";
            try
            {
                return Ok(getCount(sqlQuery));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/NoFeverCount")]
        public IActionResult NoFeverCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE [hasFever]='False'";
            try
            {
                return Ok(getCount(sqlQuery));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/TestCountToday")]
        public IActionResult TestCountToday()
        {
            string dateToday = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dateTomorrow = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd HH:mm:ss.fff");
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE [timeOfDataRecording]>=@dateToday and [timeOfDataRecording]<=@dateTomorrow";
            try
            {
                return Ok(getCount(sqlQuery, ("@dateToday",dateToday), ("@dateTomorrow", dateTomorrow)));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/FeverCountToday")]
        public IActionResult FeverCountToday()
        {
            string dateToday = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dateTomorrow = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd HH:mm:ss.fff");
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE ([timeOfDataRecording]>=@dateToday and [timeOfDataRecording]<=@dateTomorrow) AND [hasFever]='TRUE'";
            try
            {
                return Ok(getCount(sqlQuery, ("@dateToday", dateToday), ("@dateTomorrow", dateTomorrow)));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/HighestTemperature")]
        public IActionResult HighestTemperature()
        {
            string sqlQuery = "select * from Tests where temperature = (select max(temperature) from Tests)";
            try
            {
                return Ok(getTestsFromDB(sqlQuery)[0]);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/HighestTemperatureToday")]
        public IActionResult HighestTemperatureToday()
        {
            string dateToday = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dateTomorrow = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd HH:mm:ss.fff");
            string sqlQuery = "select * from Tests where temperature = (select max(temperature) from Tests where [timeOfDataRecording]>=@dateToday and [timeOfDataRecording]<=@dateTomorrow)";
            try
            {
                return Ok(getTestsFromDB(sqlQuery, ("@dateToday", dateToday), ("@dateTomorrow", dateTomorrow))[0]);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // POST api/<TestsController>
        [HttpPost("withDateTime/")]
        public IActionResult PostWDateTime([FromBody] Test value)
        {
            string insertTestSql =
                "insert into Tests (temperature, timeOfDataRecording, RPI_ID, hasFever, temperaturef) output inserted.id values (@temperature, @timeOfDataRecording, @RPI_ID, @hasFever, @temperaturef)";
            var postResults = StaticMethods.PostToDB(insertTestSql, ("@temperature", value.Temperature), ("@timeOfDataRecording", value.TimeOfDataRecording), 
                ("@RPI_ID", value.RaspberryPiID), ("@hasFever", value.HasFever), ("@temperaturef", value.TemperatureF));
            if (postResults.Item1 == staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE)
                return BadRequest();
            value.ID = postResults.Item2;
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // POST api/<TestsController>
        [HttpPost]
        public IActionResult Post([FromBody] Test value)
        {
            string insertTestSql =
                "insert into Tests (temperature, RPI_ID, hasFever, temperaturef) output inserted.id values (@temperature, @RPI_ID, @hasFever, @temperaturef)";
            var postResults = StaticMethods.PostToDB(insertTestSql, ("@temperature", value.Temperature), ("@RPI_ID", value.RaspberryPiID), ("@hasFever", value.HasFever),
                ("@temperaturef", value.TemperatureF));
            if (postResults.Item1 == staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE)
                return BadRequest();
            value.ID = postResults.Item2;
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/<TestsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Test value)
        {
            if (id != value.ID)
                return BadRequest();
            var getTest = Get(id);
            if (getTest.GetType() == typeof(NotFoundResult))
                return NotFound();
            string updateUserSql =
                "update Tests set temperature=@temperature, timeOfDataRecording=@timeOfDataRecording, RPI_ID=@RPI_ID, hasFever=@hasFever, temperaturef=@temperaturef where id=@id";
            StaticMethods.updateOrDeleteFromDB(updateUserSql, ("@temperature", value.Temperature), ("@timeOfDataRecording", value.TimeOfDataRecording), 
                ("@RPI_ID", value.RaspberryPiID), ("@hasFever", value.HasFever), ("@id", value.ID), ("@temperaturef", value.TemperatureF));
            return Ok();
        }

        // DELETE api/<TestsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var getTest = Get(id);
            if (getTest.GetType() == typeof(NotFoundResult))
                return NotFound();
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
                            temp.TemperatureF = reader.GetDouble(5);

                            tests.Add(temp);
                        }
                    }
                }
            }
            return tests;
        }




        private int getCount(string sqlCommand, params (string, object)[] parameterTuples)
        {
            int count =0;
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                using (SqlCommand selectCommand = new SqlCommand(sqlCommand, databaseConnection))
                {
                    databaseConnection.Open();
                    foreach (var pTuple in parameterTuples)
                    {
                        selectCommand.Parameters.AddWithValue(pTuple.Item1, pTuple.Item2);
                    }
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);
                        }
                    }
                }
            }
            return count;
        }
    }
}
