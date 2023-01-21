using Dental_Clinic.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dental_Clinic.Core
{
    public class SessionWorker
    {
        private HttpContext Context;
        public SessionWorker(HttpContext context)
        {
            Context = context;
        }
        public void SaveToken(string token)
        {
            Context.Session.SetString("token", $"Bearer {token}");
        }

        public void SaveUserModel(AuthUserModel user)
        {
            Context.Session.SetString("user", JsonSerializer.Serialize<AuthUserModel>(user));
        }

        public int GetUserId()
        {
            var user = Context.Session.GetString("user");
            if(user!=null)
            { 
            return JsonSerializer.Deserialize<AuthUserModel>(user).ID;
            }
            return -1;
        }
        public string GetUserName()
        {
            var user = Context.Session.GetString("user");
            if (user != null)
            {
                return JsonSerializer.Deserialize<AuthUserModel>(user).name;
            }
            return "";
        }
        public bool IsAdmin()
        {
            var user = Context.Session.GetString("user");
            if (user != null)
            {
                return (JsonSerializer.Deserialize<AuthUserModel>(user).role == role.Admin);
            }
            return false;
        }
    }
}
