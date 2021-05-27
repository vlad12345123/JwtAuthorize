using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistLabProject.Auth
{
    public class AuthOptions
    {
        public const string Issuer = "Project"; 
        public const string Audience = "Auth"; 
        const string Key = "Secretkey";  
        public const int LifeTime = 60; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
