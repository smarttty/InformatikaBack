using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestODataBackend.Models;

namespace TestODataBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private testContext db;
        public AuthController(testContext db)
        {
            this.db = db;
        }


        /*
         * GET
         * /auth/login
         */
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return Ok(HttpContext.User.Identity.Name);
        }

        /*
         * POST
         * /auth/login
         */
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]Login credentials)
        {
            User user = db.Users.FirstOrDefault(u => u.Login == credentials.UserName && u.Password == credentials.Password);
            if(user != null)
            {
                await Authenticate(credentials.UserName);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User credentials)
        {
            User user = db.Users.FirstOrDefault(u => u.Login == credentials.Login);
            if (user != null)
            {
                return BadRequest("Пользователь с таким логином уже существует");
            }

            db.Users.Add(credentials);
            db.SaveChanges();
            await Authenticate(credentials.Login);
            return Ok();
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


    }
}
