using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bike_EShop.Web.Models;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Web.Models.Home;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bike_EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRandomGeneratorService _randomGenerator;
        private readonly IBikeCountService _bikeCount;

        public HomeController(
            ILogger<HomeController> logger,
            IRandomGeneratorService randomGenerator,
            IBikeCountService bikeCount)
        {
            _logger = logger;
            this._randomGenerator = randomGenerator;
            this._bikeCount = bikeCount;
        }

        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel()
            {
                RandomNr = _randomGenerator.GenerateRandomPositiveNumber(_bikeCount.Count())
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
