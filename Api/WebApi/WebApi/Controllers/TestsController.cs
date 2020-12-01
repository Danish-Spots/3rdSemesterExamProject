using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
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
        public int TestCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM TESTS";

            try
            {
               return getCount(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("/FeverCount")]
        public int FeverCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE [hasFever]='True'";
            try
            {
                return getCount(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("/NoFeverCount")]
        public int NoFeverCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE [hasFever]='False'";
            try
            {
                return getCount(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("/TestCountToday")]
        public int TestCountToday()
        {
            DateTime dateToday = DateTime.Today;
            DateTime dateTomorrow = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE [timeOfDataRecording]>='@dateToday' and [timeOfDataRecording]<'@dateTomorrow'";
            try
            {
                return getCount(sqlQuery, ("@dateToday",dateToday), ("@dateTomorrow", dateTomorrow));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("/FeverCountToday")]
        public int FeverCountToday()
        {
            DateTime dateToday = DateTime.Today;
            string sqlQuery = "SELECT COUNT(*) FROM TESTS WHERE timeOfDataRec=@dateToday AND [hasFever]='TRUE'";
            try
            {
                return getCount(sqlQuery, ("@dateToday", dateToday));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("/HighestTemperature")]
        public double HighestTemperature()
        {
            DateTime dateToday = DateTime.Today;
            string sqlQuery = "SELECT MAX(temperature) FROM TESTS ";
            try
            {
                return getCount(sqlQuery, ("@dateToday", dateToday));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("/HighestTemperatureToday")]
        public double HighestTemperatureToday()
        {
            DateTime dateToday = DateTime.Today;
            string sqlQuery = "SELECT MAX(temperature) FROM TESTS WHERE timeOfDataRec=@dateToday";
            try
            {
                return getCount(sqlQuery, ("@dateToday", dateToday));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("/MostDangerousLocation")]
        public string MostDangerousLocation()
        {
            string sqlQuery = "Select Top 1 [location] from RaspberryPis inner join (select RPI_Id, count(RPI_Id) as 'Total' from tests where hasFever = 'true' group by RPI_Id) as query1 on query1.RPI_Id = RaspberryPis.ID order by Total desc";
            string location ="no location";
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                using (SqlCommand selectCommand = new SqlCommand(sqlQuery, databaseConnection))
                {
                    databaseConnection.Open();
                
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            location = reader.GetString(0);
                        }
                    }
                }
            }
            return location;
        }







        // POST api/<TestsController>
        [HttpPost("withDateTime/")]
        public IActionResult PostWDateTime([FromBody] Test value)
        {
            string insertTestSql =
                "insert into Tests (temperature, timeOfDataRecording, RPI_ID, hasFever, temperaturef) values (@temperature, @timeOfDataRecording, @RPI_ID, @hasFever, @temperaturef)";
            var postResults = StaticMethods.PostToDB(insertTestSql, ("@temperature", value.Temperature), ("@timeOfDataRecording", value.TimeOfDataRecording), 
                ("@RPI_ID", value.RaspberryPiID), ("@hasFever", value.HasFever), ("@temperaturef", value.TemperatureF));
            if (postResults == staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE)
                return BadRequest();
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // POST api/<TestsController>
        [HttpPost]
        public IActionResult Post([FromBody] Test value)
        {
            string insertTestSql =
                "insert into Tests (temperature, RPI_ID, hasFever, temperaturef) values (@temperature, @RPI_ID, @hasFever, @temperaturef)";
            var postResults = StaticMethods.PostToDB(insertTestSql, ("@temperature", value.Temperature), ("@RPI_ID", value.RaspberryPiID), ("@hasFever", value.HasFever),
                ("@temperaturef", value.TemperatureF));
            if (postResults == staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE)
                return BadRequest();
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
