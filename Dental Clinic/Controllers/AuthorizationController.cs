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
        public static IConfiguration _configuration { get; set; }
        private ApplicationDbContext dbContext { get; set; }

        public AuthorizationController(ApplicationDbContext _dbContext, IConfiguration configuration)
        {
            dbContext = _dbContext;
            _configuration = configuration;
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
                var token = GenerateJWTToken(employee);

             return Ok(token);
            }
            return StatusCode(401);
        }

        private string GenerateJWTToken(EmployeeModel employee)
        {
            var Tokenhendler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:key"]);
            var descriptor = new SecurityTokenDescriptor
            {
               Claims= new Dictionary<string, object>
               {
                   {"Role",employee.Role},
                   {"ID",employee.ID.ToString()}
               },
                Audience = employee.ID.ToString(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = Tokenhendler.CreateToken(descriptor);
            return Tokenhendler.WriteToken(token);
        }
    }
}
