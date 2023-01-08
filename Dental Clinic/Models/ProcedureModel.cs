using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Models
{
    public class ProcedureModel
    {
        [Key]
        [AutoIncrement]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int NeedHoursOnProcedure { get; set; }
        
        [Required]
        public int NeedMinutesOnProcedure { get; set; }

        [Required]
        public List<EmployeeModel> Doctors { get; set; }

    }
}
