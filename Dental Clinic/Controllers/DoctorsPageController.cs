using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dental_Clinic.Controllers
{
    [Authorize]
    public class DoctorsPageController : Controller
    {

        private ApplicationDbContext dbContext;

        public DoctorsPageController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        [HttpGet("DoctorsPage")]
        public ActionResult Index()
        {
           
            var sessionWorker = new SessionWorker(HttpContext);
            
            string NameOfDoctor = sessionWorker.GetUserName();

            List<EntryFormModel> records = dbContext.ListOfRecords.
                Where(x=>x.Doctor==NameOfDoctor).
                      OrderByDescending(x => x.ID).ToList();
           
            return View(records);
        }



        [HttpGet("DoctorsPage/Details/{id?}")]
        public ActionResult Details(int id)
        {
            EntryFormModel record = dbContext.ListOfRecords.First(x => x.ID == id);

            return View(record);
        }


        [HttpGet("DoctorsPage/Edit/{id?}")]
        public ActionResult Edit(int id,string Time_problem_message)
        {
           

            ViewData["ListOfDoctors"] = dbContext.employees.Where(e => e.Role == role.Doctor).ToList();

            ViewData["ListOfProcedures"] = dbContext.ListOfProcedure.ToList();

            ViewData["Time_problem_message"] = Time_problem_message;

            EntryFormModel record = dbContext.ListOfRecords.First(x => x.ID == id);

            ViewData["Date-Time"] = $"{record.StartOfProcedure.Year}-{record.StartOfProcedure.Month}-{record.StartOfProcedure.Day}T{record.StartOfProcedure.Hour}:{record.StartOfProcedure.Minute}";

            return View(record);
        }

        [HttpPost("DoctorsPage/Edit/{id?}")]
        public ActionResult Edit(int id, EntryFormModel record)
        {
            string Time_problem_message = null;

            if (ModelState.IsValid)
            {
                ProcedureModel procedure = dbContext.ListOfProcedure.First(e => e.Name == record.Procedure);
                Time_problem_message = CheckTimeHelper.Check(
                  record,
                  procedure.NeedHoursOnProcedure,
                  procedure.NeedMinutesOnProcedure,
                  dbContext.ListOfRecords.ToList()
                      );


                if (Time_problem_message == null)
                {
                    dbContext.Remove(dbContext.ListOfRecords.First(x=> x.ID==record.ID));
                    dbContext.ListOfRecords.Add(record);
                    dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return Edit(record.ID,Time_problem_message);
        }



        [HttpPost("DoctorsPage/Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            dbContext.ListOfRecords.Remove(dbContext.ListOfRecords.First(x => x.ID == id));
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
