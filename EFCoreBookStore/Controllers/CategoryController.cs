using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EFCoreBookStore.Models;

namespace EFCoreBookStore.Controllers
{
    public class CategoryController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var items = db.Categories.Select(p => p);

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
        public IActionResult Create(Category item)
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
            var item = db.Categories.SingleOrDefault(p=>p.CategoryID.Equals(id));

            return View(item);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("CategoryID,Name")] Category item)
        {
            if(ModelState.IsValid){
                db.Update(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var item = db.Categories.Find(id);
                db.Categories.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
