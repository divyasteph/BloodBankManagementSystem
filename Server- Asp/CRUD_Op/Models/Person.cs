using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Op.Models
{
    public class Person
    {
        public int Person_ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string IsActive { get; set; }
        public string Password { get; set; }
        
    }
}