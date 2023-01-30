using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [AllowAnonymous]
        [HttpGet("ListOfDoctors/Details/{id?}")]
        public ActionResult Details(int id)
        {
            return View();
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



        [HttpDelete("ListOfDoctors/Delete/{id?}")]
        public ActionResult Delete(int id)
        {          
          return RedirectToAction(nameof(Index));          
        }
    }
}
