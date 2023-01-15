using Dental_Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet("Booking")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Booking")]
        public IActionResult Index(EmployeeModel employee)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
