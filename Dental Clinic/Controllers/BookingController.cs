using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.Configuration;
using System.Linq;

namespace Dental_Clinic.Controllers
{
    public class BookingController : Controller
    {
        private ApplicationDbContext dbContext;
        private SendGridSendler Sendler;

        public BookingController(ApplicationDbContext _dbContext,IConfiguration configuration)
        {
            dbContext = _dbContext;
            Sendler = new SendGridSendler(configuration);
        }


        [HttpGet("Booking")]
        public IActionResult Index(string Time_problem_message)
        {
            var sessionWorker = new SessionWorker(HttpContext);

            ViewData["Authorized"] = sessionWorker.IsAuthorized();

            ViewData["ListOfDoctors"] = dbContext.employees.Where(e => e.Role == role.Doctor).ToList();

                ViewData["ListOfProcedures"] = dbContext.ListOfProcedure.ToList();

                ViewData["Time_problem_message"] = Time_problem_message;

            return View();
        }

        [HttpPost("Booking")]
        public IActionResult Index(EntryFormModel entryForm)
        {  
            string Time_problem_message = null;
            var sessionWorker = new SessionWorker(HttpContext);

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

                    //if user is Authorized we don't send email on corporate mailbox
                    if (!sessionWorker.IsAuthorized())
                    { 
                      Sendler.SendRegistrationEmail(entryForm);
                    }

                    return RedirectToAction("success");
                }
            }
            return Index(Time_problem_message);
        }

        [HttpGet("Booking/success")]
        public IActionResult success()
        {
            return View();
        }
    }
}
