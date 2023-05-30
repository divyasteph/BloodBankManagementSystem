using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Op.Models
{
    public class Request
    {
        public int Receiver_ID { get; set; }
        public string Name { get; set; }
        public string BloodGroup { get; set; }
        public int Unit { get; set; }
        public string Hospital { get; set; }
        public int Phone { get; set; }
        public string Status { get; set; }

        public int Unit_Available { get; set; }
    }
}