using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Test
    {
        public int ID { get; set; }
        public double Temperature { get; set; }
        public DateTime TimeOfDataRecording { get; set; }
        public int RaspberryPiID { get; set; }
        public bool HasFever { get; set; }

        public double TemperatureF { get; set; }
    }
}
