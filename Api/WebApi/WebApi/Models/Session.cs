using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Session
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public int UserID { get; set; }
    }
}
