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
    public class ListOfProceduresController : Controller
    {
        private ApplicationDbContext dbContext;

        public ListOfProceduresController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [AllowAnonymous]
        [HttpGet("ListOfProcedures")]
        public ActionResult Index()
        {
            //Check if have acсess to details
            var sessionWorker = new SessionWorker(HttpContext);
            ViewData["IsAdmin"] = sessionWorker.IsAdmin();

            List<ProcedureModel> Procedures = dbContext.ListOfProcedure.ToList();

            return View(Procedures);
        }


        [HttpGet("ListOfProcedures/Details/{id?}")]
        public ActionResult Details(int id)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
            ProcedureModel procedure = dbContext.ListOfProcedure.First(x => x.ID == id);
            return View(procedure);
            }
            return StatusCode(401);
        }



        [HttpGet("ListOfProcedures/Create")]
        public ActionResult Create()
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                return View();
            }
            return StatusCode(401);
        }

        [HttpPost("ListOfProcedures/Create")]
        public ActionResult Create(ProcedureModel procedure)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                dbContext.ListOfProcedure.Add(procedure);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            } 
            return StatusCode(401);
        }
           
    


        [HttpGet("ListOfProcedures/Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                ProcedureModel procedure = dbContext.ListOfProcedure.First(x => x.ID == id);
                return View(procedure);
            }
            return StatusCode(401);
        }

        [HttpPost("ListOfProcedures/Edit/{id?}")]
        public ActionResult Edit(int id, ProcedureModel procedure)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                dbContext.ListOfProcedure.Update(procedure);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return StatusCode(401);
        }



        [HttpPost("ListOfProcedures/Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                ProcedureModel procedure = dbContext.ListOfProcedure.First(x => x.ID == id);
                 dbContext.ListOfProcedure.Remove(procedure);
                 dbContext.SaveChanges();
                 return RedirectToAction(nameof(Index));
            }
            return StatusCode(401);
        }
    }
}
