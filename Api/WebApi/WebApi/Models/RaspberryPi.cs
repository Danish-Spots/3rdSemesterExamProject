using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class RaspberryPi
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public int ProfileID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
