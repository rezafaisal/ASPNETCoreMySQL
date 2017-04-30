using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using EFCoreBookStore.Models;

namespace EFCoreBookStore.Controllers
{
    public class ContohOtentikasiController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginFormViewModel item)
        {            
            if(ModelState.IsValid){
                if(item.UserName.Equals("admin") && item.Password.Equals("rahasia")){

                    var claims = new List<Claim>
                    {
                        new Claim("username", "admin"),
                        new Claim(ClaimTypes.Name, "M Reza Faisal"),
                        new Claim(ClaimTypes.Email, "admin@faisal.net"),
                        new Claim(ClaimTypes.Role, "admin")
                    };

                    var id = new ClaimsIdentity(claims, "password");
                    var principal = new ClaimsPrincipal(id);

                    await HttpContext.Authentication.SignInAsync("Cookies", principal);

                    return RedirectToAction("Secret");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
    }
}