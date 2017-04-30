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
    public class HomeController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();
        string key = "E546C8DF278CD5931069B522E695D4F2";
        public static string DecryptString(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginFormViewModel item, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if(ModelState.IsValid){
                var user = db.Users.SingleOrDefault(p => p.UserName.Equals(item.UserName));
                if(user != null){
                    if(!String.IsNullOrEmpty(user.Password)){
                        string decryptPassword = DecryptString(user.Password, key);

                        if(item.Password.Equals(decryptPassword)){
                            var role = db.Roles.SingleOrDefault(p => p.RoleID.Equals(user.RoleID));

                            var claims = new List<Claim>
                            {
                                new Claim("username", user.UserName),
                                new Claim("fullname", user.Fullname),
                                new Claim(ClaimTypes.Role, role.RoleName)
                            };

                            var id = new ClaimsIdentity(claims, "password");
                            var principal = new ClaimsPrincipal(id);

                            await HttpContext.Authentication.SignInAsync("Cookies", principal);

                            return RedirectToAction("Index");
                        }
                    }
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
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
