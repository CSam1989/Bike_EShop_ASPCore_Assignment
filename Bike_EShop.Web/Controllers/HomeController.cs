using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bike_EShop.Web.Models;
using Bike_EShop.Application.Common.Interfaces;
using System.IO;
using Bike_EShop.Web.Models.Home;

namespace Bike_EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRandomGeneratorService _randomGenerator;

        public HomeController(
            ILogger<HomeController> logger,
            IRandomGeneratorService randomGenerator)
        {
            _logger = logger;
            this._randomGenerator = randomGenerator;
        }

        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel()
            {
                RandomNr = _randomGenerator.GenerateRandomNumber(Directory.GetFiles("./wwwroot/images/bikes").Length)
            };

            return View(vm);
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
