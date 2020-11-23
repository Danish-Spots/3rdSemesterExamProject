using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string companyName { get; set; }
        public string city { get; set; }
        public DateTime joinDate { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string country { get; set; }
    }
}
