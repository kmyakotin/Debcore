using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Debweb.Models;
using MongoDB.Driver;

namespace Debweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMongoDb _db;

        public HomeController(IMongoDb db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = _db.Client.ListDatabases().ToList().First()["name"];
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
