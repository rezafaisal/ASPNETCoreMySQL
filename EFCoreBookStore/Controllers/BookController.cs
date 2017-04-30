using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using EFCoreBookStore.Models;

namespace EFCoreBookStore.Controllers
{
    public class BookController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();
        private IHostingEnvironment _environment;

        public BookController(IHostingEnvironment environment){
            _environment = environment;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var bookList = db.Books.ToList();
            
            IList<BookViewModel> items = new List<BookViewModel>();
            foreach(Book book in bookList){
                BookViewModel item = new BookViewModel();

                item.ISBN = book.ISBN;
                item.Title = book.Title;
                item.Photo = book.Photo;
                item.PublishDate = book.PublishDate;
                item.Price = book.Price;
                item.Quantity = book.Quantity;
                
                var category = db.Categories.Where(p=>p.CategoryID.Equals(book.CategoryID)).Single<Category>();
                item.CategoryName = category.Name;

                string authorNameList = string.Empty;
                var booksAuthorsList = db.BooksAuthors.Where(p=>p.ISBN.Equals(book.ISBN));
                foreach(BookAuthor booksAuthors in booksAuthorsList){
                    BookStoreDataContext db2 = new BookStoreDataContext();
                    var author = db2.Authors.Where(p=>p.AuthorID.Equals(booksAuthors.AuthorID)).Single<Author>();
                    authorNameList = authorNameList + author.Name + ", ";
                }
                item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

                items.Add(item);
            }

            return View(items);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryID", "Name"); 
            ViewBag.Authors = new MultiSelectList(db.Authors.ToList(), "AuthorID", "Name"); 

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(BookFormViewModel item)
        {
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryID", "Name"); 
            ViewBag.Authors = new MultiSelectList(db.Authors.ToList(), "AuthorID", "Name"); 
            
            if(ModelState.IsValid){
                Book book = new Book();
                book.ISBN = item.ISBN;
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublishDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;
                db.Add(book);
                
                foreach(int authorId in item.AuthorIDs){
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.ISBN = item.ISBN;
                    bookAuthor.AuthorID = authorId;
                    db.Add(bookAuthor);
                }

                db.SaveChanges();

                if(item.Photo != null){
                    var file = item.Photo;
                    var uploads = Path.Combine(_environment.WebRootPath, "upload");
                    if (file.Length > 0){
                        using (var fileStream = new FileStream(Path.Combine(uploads, item.ISBN+".jpg"), FileMode.Create)){
                            file.CopyToAsync(fileStream);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryID", "Name"); 
            ViewBag.Authors = new MultiSelectList(db.Authors.ToList(), "AuthorID", "Name"); 
            
            var book = db.Books.SingleOrDefault(p=>p.ISBN.Equals(id));
            
            BookFormViewModel item = new BookFormViewModel();
            item.ISBN = book.ISBN;
            item.Title = book.Title;
            item.PublishDate = book.PublishDate;
            item.Price = book.Price;
            item.Quantity = book.Quantity;
            item.CategoryID = book.CategoryID;

            var authorList = db.BooksAuthors.Where(p => p.ISBN.Equals(book.ISBN)).ToList();
            List<int> authors = new List<int>();
            foreach(BookAuthor bookAuthor in authorList){
                authors.Add(bookAuthor.AuthorID);
            }
            item.AuthorIDs = authors.ToArray();

            return View(item);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ISBN, CategoryID, Title, Photo, PublishDate, Price, Quantity, AuthorIDs")] BookFormViewModel item)
        {
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryID", "Name"); 
            ViewBag.Authors = new MultiSelectList(db.Authors.ToList(), "AuthorID", "Name"); 

            if(ModelState.IsValid){
                db.BooksAuthors.RemoveRange(db.BooksAuthors.Where(p => p.ISBN.Equals(item.ISBN)));
                db.SaveChanges();

                Book book = db.Books.SingleOrDefault(p => p.ISBN.Equals(item.ISBN));
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublishDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;
                db.Update(book);

                foreach(int authorId in item.AuthorIDs){
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.ISBN = item.ISBN;
                    bookAuthor.AuthorID = authorId;
                    db.Add(bookAuthor);
                }

                db.SaveChanges();

                if(item.Photo != null){
                    var file = item.Photo;
                    var uploads = Path.Combine(_environment.WebRootPath, "upload");
                    if (file.Length > 0){
                        using (var fileStream = new FileStream(Path.Combine(uploads, item.ISBN+".jpg"), FileMode.Create)){
                            file.CopyToAsync(fileStream);
                        }
                    }
                }

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
                var item = db.Books.Find(id);
                db.BooksAuthors.RemoveRange(db.BooksAuthors.Where(p => p.ISBN.Equals(item.ISBN)));
                db.SaveChanges();

                db.Books.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}