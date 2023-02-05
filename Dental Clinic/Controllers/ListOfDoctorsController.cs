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

            List<EmployeeModel> employees;
            if (sessionWorker.IsAdmin())
            {
             employees  = dbContext.employees.ToList();
            }
             else
             {
              employees = dbContext.employees.Where(x => x.Role == role.Doctor).ToList();
             }
            return View(employees);
        }



        [HttpGet("ListOfDoctors/Details/{id?}")]
        public ActionResult Details(int id)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                EmployeeModel doctor = dbContext.employees.First(x => x.ID == id);
                return View(doctor);
            }
            return StatusCode(401);
        }



        [HttpGet("ListOfDoctors/Create")]
        public ActionResult Create()
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                return View();
            }
            return StatusCode(401);
        }
      
        [HttpPost("ListOfDoctors/Create")]
        public ActionResult Create(EmployeeModel employee)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                dbContext.employees.Add(employee);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return StatusCode(401);
        }


        [HttpGet("ListOfDoctors/Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                EmployeeModel doctor = dbContext.employees.First(x => x.ID == id);

                return View(doctor);
            }
            return StatusCode(401);
        }

        [HttpPost("ListOfDoctors/Edit/{id?}")]
        public ActionResult Edit(int id,EmployeeModel employee)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                dbContext.employees.Update(employee);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return StatusCode(401);
        }



        [HttpPost("ListOfDoctors/Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            var sessionWorker = new SessionWorker(HttpContext);
            if (sessionWorker.IsAdmin())
            {
                EmployeeModel employee = dbContext.employees.First(x => x.ID == id);
                dbContext.employees.Remove(employee);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return StatusCode(401);
        }
    }
}
