using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Dental_Clinic.Controllers
{
    public class AuthorizationController : Controller
    {
       
        private ApplicationDbContext dbContext { get; set; }
        public static IConfiguration configuration { get; set; }

        public AuthorizationController(ApplicationDbContext _dbContext, IConfiguration _configuration)
        {
            dbContext = _dbContext;
            configuration = _configuration;
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
                var Geterator = new GeneratorJWTTokens(configuration);
                var token = Geterator.GenerateJWTToken(employee);
                HttpContext.Request.Headers["Authorization"] =$"Bearer {token}" ;
                
                
             return View();
            }
            return StatusCode(401);
        }

    }
}
