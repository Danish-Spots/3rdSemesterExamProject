using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUDPServer.Model
{
    public class Test
    {
        public Test(double temperature, int id, int rpiId, bool hasFever, DateTime timeOfDataRec)
        {
            Temperature = temperature;
            ID = id;
            Rpi_ID = rpiId;
            HasFever = hasFever;
            TimeOfDataRec = timeOfDataRec;
        }

        public Test() { }

        public double Temperature { get; set; }
        public int ID { get; set; }
        public int Rpi_ID { get; set; }
        public bool HasFever { get; set; }
        public DateTime TimeOfDataRec { get; set; }
    }
}
