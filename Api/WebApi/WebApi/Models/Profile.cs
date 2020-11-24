using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public DateTime JoinDate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
