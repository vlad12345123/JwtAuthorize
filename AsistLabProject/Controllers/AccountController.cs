using AsistLabProject.Auth;
using AsistLabProject.Models;
using AsistLabProject.Models.Data;
using AsistLabProject.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AsistLabProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUser user;

        public AccountController(IUser user)
        {
            this.user = user;
        }

        public User Authenticate(string name, string password)
        {
            return user.GetUsers().FirstOrDefault(u => u.Name == name && u.Password == password);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {
            var user = Authenticate(name, password);

            if(user != null)
            {
                var token = GenerateToken(user);

                return Ok(new
                {
                    access_token = token
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            
            var generateJwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(generateJwt);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
