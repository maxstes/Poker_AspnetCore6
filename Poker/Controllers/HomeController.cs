using Microsoft.AspNetCore.Mvc;
using Poker.Models;
using System.Diagnostics;

namespace Poker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string? EmailSession = HttpContext.Session.GetString("Email");
            string? Email = User.Identity.Name;

            if (string.IsNullOrEmpty(EmailSession))
            {
                if (User.Identity.IsAuthenticated)
                {
                    HttpContext.Session.SetString("Email", Email!);
                    return View(Email as object);
                }

                return View("Guest" as object);

            }
            return View(EmailSession as object);
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