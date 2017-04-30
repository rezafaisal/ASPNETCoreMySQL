using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EFCoreBookStore.Models;

namespace EFCoreBookStore.Controllers
{
    public class RoleController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var items = db.Roles.Select(p => p);

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Role item)
        {
            if(ModelState.IsValid){
                db.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var item = db.Roles.SingleOrDefault(p=>p.RoleID.Equals(id));

            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("RoleID,RoleName")] Role item)
        {
            if(ModelState.IsValid){
                db.Update(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var item = db.Roles.Find(id);
                db.Roles.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}