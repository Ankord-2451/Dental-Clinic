using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Models
{
    public enum role
    {
        Admin,
        Doctor
    }

    public class EmployeeModel
    {
        [Key]
        [AutoIncrement]
        public int ID { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { set; get; }

        [Required]
        public role Role { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Login { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
