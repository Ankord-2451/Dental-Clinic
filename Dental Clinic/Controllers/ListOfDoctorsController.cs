using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Dental_Clinic.Controllers
{
    [Authorize]
    public class ListOfDoctorsController : Controller
    {
        private ApplicationDbContext dbContext;

        public ListOfDoctorsController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [AllowAnonymous]
        [HttpGet("ListOfDoctors")]
        public ActionResult Index()
        {
            //Check if have acсess to details
            var sessionWorker = new SessionWorker(HttpContext);
            ViewData["IsAdmin"] = sessionWorker.IsAdmin();

            List<EmployeeModel> doctors  =dbContext.employees.Where(x => x.Role == role.Doctor).ToList();
           
            return View(doctors);
        }



        [HttpGet("ListOfDoctors/Details/{id?}")]
        public ActionResult Details(int id)
        {
            EmployeeModel doctor = dbContext.employees.First(x => x.ID == id);
            return View(doctor);
        }



        [HttpGet("ListOfDoctors/Create")]
        public ActionResult Create()
        {
            return View();
        }
      
        [HttpPost("ListOfDoctors/Create")]
        public ActionResult Create(EmployeeModel employee)
        {         
            return RedirectToAction(nameof(Index));          
        }


        [HttpGet("ListOfDoctors/Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost("ListOfDoctors/Edit/{id?}")]
        public ActionResult Edit(int id,EmployeeModel employee)
        {          
                return RedirectToAction(nameof(Index));
        }



        [HttpPost("ListOfDoctors/Delete/{id?}")]
        public ActionResult Delete(int id)
        {          
          return RedirectToAction(nameof(Index));          
        }
    }
}
