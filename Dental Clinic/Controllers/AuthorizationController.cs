using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Dental_Clinic.Controllers
{
    public class AuthorizationController : Controller
    {
       
        private ApplicationDbContext dbContext { get; set; }

        public AuthorizationController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet("Authorization/Form")]
        public ActionResult Index()
        {
           return View();
        }

        [HttpPost("Authorization/Form")]
        public ActionResult Index(string login,string password)
        {
            EmployeeModel employee;

            try { 
            employee = dbContext.employees.First(e => (e.Login == login) && (e.Password == password));
            }
            catch
            {
                employee = null;
            }

            if(employee != null) 
            {
                var token = GeneratorJWTTokens.GenerateJWTToken(employee);
                HttpContext.Request.Headers["Authorization"] =$"Bearer {token}" ;
                
                
             return View();
            }
            return StatusCode(401);
        }

    }
}
