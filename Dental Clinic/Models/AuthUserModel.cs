using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Models
{
    public class AuthUserModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public role role { get; set; }
    }
}
