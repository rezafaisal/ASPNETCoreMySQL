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
using Microsoft.AspNetCore.Authorization;
using EFCoreBookStore.Models;

namespace EFCoreBookStore.Controllers
{
    public class UserController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();
        string key = "E546C8DF278CD5931069B522E695D4F2";

        public static string EncryptString(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var userList = db.Users.ToList();
            IList<UserViewModel> items = new List<UserViewModel>();
            foreach(User user in userList){
                UserViewModel item = new UserViewModel();

                item.UserName = user.UserName;
                item.Email = user.Email;
                item.Fullname = user.Fullname;

                Role role = db.Roles.SingleOrDefault(p => p.RoleID.Equals(user.RoleID));
                item.RoleName = role.RoleName;

                items.Add(item);
            }

            return View(items);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleID", "RoleName"); 

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(UserCreateFormViewModel item)
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleID", "RoleName"); 

            if(ModelState.IsValid){
                User user = new User();
                user.UserName = item.UserName;
                user.RoleID = item.RoleID;
                user.Email = item.Email;
                user.Password = EncryptString(item.Password, key);
                user.Fullname = item.Fullname;

                db.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleID", "RoleName"); 

            var user = db.Users.SingleOrDefault(p=>p.UserName.Equals(id));
            UserEditFormViewModel item = new UserEditFormViewModel();
            item.UserName = user.UserName;
            item.RoleID = user.RoleID;
            item.Email = user.Email;
            item.Fullname = user.Fullname;

            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("UserName, RoleID, Email, Password, PasswordConfirm, Fullname")] UserEditFormViewModel item)
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleID", "RoleName"); 

            if(ModelState.IsValid){
                User user = db.Users.SingleOrDefault(p => p.UserName.Equals(item.UserName));
                user.UserName = item.UserName;
                user.RoleID = item.RoleID;
                user.Email = item.Email;
                if(!String.IsNullOrEmpty(item.Password)){
                    user.Password = EncryptString(item.Password, key);
                }
                user.Fullname = item.Fullname;

                db.Update(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            if(ModelState.IsValid)
            {
                var item = db.Users.Find(id);
                db.Users.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}