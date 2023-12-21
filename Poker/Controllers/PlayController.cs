using Microsoft.AspNetCore.Mvc;
using Poker.Models.Game;
using Poker.Services;

namespace Poker.Controllers
{
    public class PlayController : Controller
    {
        PlayServices PlayServices = new PlayServices();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Yes(int userId,int roomId)
        {
            
            if(userId == 0 && roomId == 0)
            {
                throw new Exception("Yes argument = null");
            }
            PlayServices.JoinRoom(userId, roomId);

            return View();
            //TODO Заклепать
        }
        public IActionResult No()
        {
            return RedirectToAction("Index","Home");
        }
    }
}
