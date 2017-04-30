using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EFCoreBookStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace EFCoreBookStore.Controllers
{
    public class AuthorController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var items = db.Authors.Select(p => p);

            return View(items);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Author item)
        {
            if(ModelState.IsValid){
                db.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int? id)
        {
            var item = db.Authors.SingleOrDefault(p=>p.AuthorID.Equals(id));
            
            return View(item);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("AuthorID,Name, Email")] Author item)
        {
            if(ModelState.IsValid){
                db.Update(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var item = db.Authors.Find(id);
                db.Authors.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
