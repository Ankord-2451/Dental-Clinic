using Dental_Clinic.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Dental_Clinic;
using System.Security.Claims;

namespace Dental_Clinic
{
    public class GenerateJWTToken
    {
        public IConfiguration _configuration { get; set; }
        public GenerateJWTToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       public static string GenerateJWTtoken(EmployeeModel employee)
        {
            var Tokenhendler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("asddndhfggfgdhyrtfgdfhdgdhgfdj");
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(employee.Role.ToString()),
                Audience = "Aud",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = Tokenhendler.CreateToken(descriptor);
            return Tokenhendler.WriteToken(token);
        }
    }
}
