using Microsoft.AspNetCore.Mvc;

namespace Poker.Controllers
{
    public class PlayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Yes()
        {
            return RedirectToAction("Index");
            //TODO Заклепать
        }
        public IActionResult No()
        {
            return RedirectToAction("Index");
        }
    }
}
