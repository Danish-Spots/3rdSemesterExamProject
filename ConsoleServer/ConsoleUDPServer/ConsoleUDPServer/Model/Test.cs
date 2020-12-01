using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUDPServer.Model
{
    public class Test
    {
        public Test(double temperature, int id, int rpiId, bool hasFever, DateTime timeOfDataRec, double temperatureF)
        {
            Temperature = temperature;
            ID = id;
            RaspberryPiID = rpiId;
            HasFever = hasFever;
            TimeOfDataRecording = timeOfDataRec;
            TemperatureF = temperatureF;

        }

        public Test() { }

        public double Temperature { get; set; }
        public int ID { get; set; }
        public int RaspberryPiID { get; set; }
        public bool HasFever { get; set; }
        public DateTime TimeOfDataRecording { get; set; }

        public double TemperatureF { get; set; }
    }
}
