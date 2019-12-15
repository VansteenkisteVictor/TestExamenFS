using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Data;
using TestExamen.Models;

namespace TestExamen.Controllers
{
    public class HomeController : Controller
    {
        private ILogger _logger;
        private ISeeder _seeder;
        public HomeController(ILoggerFactory loggerFactory,ISeeder seeder)
        {
            _logger = loggerFactory.CreateLogger(typeof(HomeController));
            _seeder = seeder;
        }

       

        public IActionResult Index()
        {
            _logger.LogWarning("Index Loaded");
            _seeder.initDatabase(10);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
