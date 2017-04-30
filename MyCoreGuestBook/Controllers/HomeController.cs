using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCoreGuestBook.Models;

using MySql.Data.MySqlClient;

namespace MyCoreGuestBook.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IList<GuestBook> items = new List<GuestBook>();

            // koneksi database
            MySqlConnection conn = new MySqlConnection{
                ConnectionString = Startup.ConnectionString
            };
            conn.Open();
            
            // menyiapkan query
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM guestbooks;", conn);
            
            // membaca data
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read()){
                // menyimpan record ke object model
                GuestBook item = new GuestBook();
                item.Email = Convert.ToString(dataReader["guest_email"]);
                item.Name = Convert.ToString(dataReader["guest_name"]);
                item.Message = Convert.ToString(dataReader["message"]);

                // menyimpan object model ke collection 
                items.Add(item);
            }
            dataReader.Close();

            return View(items);
        }

        [HttpGet]
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(GuestBook item){
            if(ModelState.IsValid){
                MySqlConnection conn = new MySqlConnection{
                    ConnectionString = Startup.ConnectionString
                };
                conn.Open();

                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO guestbooks (guest_name, guest_email, message) VALUES (?name, ?email, ?message)";
                command.Parameters.AddWithValue("?name", item.Name);
                command.Parameters.AddWithValue("?email", item.Email);
                command.Parameters.AddWithValue("?message", item.Message);
                command.ExecuteNonQuery();

                conn.Close();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
