using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Controllers
{  
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext;
        public HomeController(ApplicationDbContext _dbContext)
        {
            dbContext= _dbContext;
        }
        public IActionResult Index()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>()
            {
                new EmployeeModel(){Login="Dman",Password="1q2w3e",Name="Tom",Role = role.Doctor,Description="cool doctor"},
                new EmployeeModel(){Login="Aman",Password="qwe123",Name="Sam",Role = role.Admin}
            };
            List<ProcedureModel> procedures = new List<ProcedureModel>() {
                new ProcedureModel(){Name = "test1",NeedHoursOnProcedure =1,NeedMinutesOnProcedure =0,Description="1 procedure"},
                  new ProcedureModel(){Name = "test2",NeedHoursOnProcedure =2,NeedMinutesOnProcedure =30,Description="2 procedure"}
            };
            dbContext.employees.AddRange(employees);
            dbContext.ListOfProcedure.AddRange(procedures);
            dbContext.SaveChanges();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
