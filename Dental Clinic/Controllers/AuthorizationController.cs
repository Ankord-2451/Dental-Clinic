using Dental_Clinic.Core;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Dental_Clinic.Controllers
{
    public class AuthorizationController : Controller
    {

        private ApplicationDbContext dbContext;
        public static IConfiguration configuration;

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
                //Object for work with session
                var session = new SessionWorker(HttpContext);

                //Set in session JWT Token for Authorization
                var Geterator = new GeneratorJWTTokens(configuration);
                var token = Geterator.GenerateJWTToken(employee);
                
                session.SaveToken(token);
                //Set in session object type of AuthUserModel for Authentication
                session.SaveUserModel(new AuthUserModel()
                { 
                    ID = employee.ID,
                    name = employee.Name,
                    role = employee.Role 
                });

                return View();
            }
            return StatusCode(401);
        }

    }
}
