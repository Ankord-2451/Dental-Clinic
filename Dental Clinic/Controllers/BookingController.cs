﻿using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Completion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Controllers
{
    public class BookingController : Controller
    {
        private ApplicationDbContext dbContext;

        public BookingController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        [HttpGet("Booking")]
        public IActionResult Index(string Time_problem_message)
        {

                ViewData["ListOfDoctors"] = dbContext.employees.Where(e => e.Role == role.Doctor).ToList();

                ViewData["ListOfProcedures"] = dbContext.ListOfProcedure.ToList();

                ViewData["Time_problem_message"] = Time_problem_message;

            return View();
        }

        [HttpPost("Booking")]
        public IActionResult Index(EntryFormModel entryForm)
        {  
            string Time_problem_message = null;

            if (ModelState.IsValid)
            {
              
              ProcedureModel procedure = dbContext.ListOfProcedure.First(e => e.Name == entryForm.Procedure);
              Time_problem_message = CheckTimeHelper.Check(
                entryForm,
                procedure.NeedHoursOnProcedure,
                procedure.NeedMinutesOnProcedure,
                dbContext.ListOfRecords.ToList()
                    );


                if (Time_problem_message == null)
                {
                   dbContext.ListOfRecords.Add(entryForm);
                   dbContext.SaveChanges();
                }
            }
            return Index(Time_problem_message);
        }
    }
}
