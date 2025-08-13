using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using YAKBAKv2.Models;

namespace YAKBAKv2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // PUBLIC HOME PAGE
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        // PUBLIC PRIVACY PAGE
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        // PUBLIC ERROR PAGE
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
