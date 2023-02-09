

using System.Security.Cryptography;
using System;
using System.Text;
using Dental_Clinic.Models;

namespace Dental_Clinic.Core
{
    public static class Encoder
    {
       public static void Encode(EmployeeModel employee)
        { 
       // HMACSHA256 hm = new HMACSHA256(Encoding.UTF8.GetBytes(employee.Password));
       // byte[] result = hm.ComputeHash(enc.GetBytes(packet));
       // String hex = BitConverter.ToString(result);
       // hex = hex.Replace("-", "");
   // Console.WriteLine(hex.ToLower());
        }
    }
}
