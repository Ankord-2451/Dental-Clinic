using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Models
{
    public class EntryFormModel
    {
        [Key]
        [AutoIncrement]
        public int ID { get; set; }

        [Required]
        public string Procedure { get; set; }

        [Required]
        public string Doctor { get; set; }

        public DateTime StartOfProcedure { get; set; }

        public DateTime EndOfProcedure { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        public string UserPhoneNumber { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public string Comment { get; set; }
    }
}
