using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int profileID { get; set; }
    }
}
