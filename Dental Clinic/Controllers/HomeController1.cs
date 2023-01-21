using Dental_Clinic.Core;
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
    [ApiController]
    [Authorize]
    public class HomeController1 : Controller
    {
        [HttpGet("/jwt")]
        public ActionResult View1()
        {
            var session = new SessionWorker(HttpContext);
            var user = new AuthUserModel()
            {
                ID = session.GetUserId(),
                name = session.GetUserName()
            };
            return View(user);
        }

        
        
    }
}
