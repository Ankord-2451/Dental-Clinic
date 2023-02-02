using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            ProcedureModel procedure = dbContext.ListOfProcedure.First(x => x.ID == id);
            return View(procedure);
        }



        [HttpGet("ListOfProcedures/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("ListOfProcedures/Create")]
        public ActionResult Create(ProcedureModel procedure)
        {
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("ListOfProcedures/Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost("ListOfProcedures/Edit/{id?}")]
        public ActionResult Edit(int id, ProcedureModel procedure)
        {
            return RedirectToAction(nameof(Index));
        }



        [HttpDelete("ListOfProcedures/Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
