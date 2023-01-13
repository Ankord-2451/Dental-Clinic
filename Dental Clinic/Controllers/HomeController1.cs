using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Controllers
{
    [ApiController]
    [Authorize]
    public class HomeController1 : Controller
    {
        [HttpGet("/jwt")]
        public ActionResult View1()
        {
            return View();
        }

        
        
    }
}
