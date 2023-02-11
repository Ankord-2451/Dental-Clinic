using System.Security.Cryptography;
using System;
using System.Text;
using Dental_Clinic.Models;
using Microsoft.Extensions.Configuration;

namespace Dental_Clinic.Core
{
    public static class Encoder
    {
       public static void EncodeEmployee(IConfiguration config,EmployeeModel employee)
        { 
        SHA256 hm = SHA256.Create();
            //Hash password
        byte[] result = hm.ComputeHash(Encoding.UTF8.GetBytes(employee.Password));

            employee.Password = BitConverter.ToString(result).Replace("-", "") + $"{config["Encoder:SecurityKey"]}";
            //Hash login
        result = hm.ComputeHash(Encoding.UTF8.GetBytes(employee.Login));
           
            employee.Login = BitConverter.ToString(result).Replace("-", "") + $"{config["Encoder:SecurityKey"]}";
        }

        public static string Encode(IConfiguration config,string str)
        {
            SHA256 hm = SHA256.Create();
            
            byte[] result = hm.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(result).Replace("-", "") + $"{config["Encoder:SecurityKey"]}";
        }

    }
}
