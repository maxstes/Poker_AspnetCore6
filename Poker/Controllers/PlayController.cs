using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poker.Models.Game;
using Poker.Services;

namespace Poker.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        PlayServices playServices;
        public PlayController(PlayServices _playServices)
        {
            playServices = _playServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Yes(int userId,int roomId)
        {
            
            if(userId == 0 && roomId == 0)
            {
                throw new Exception("Yes argument = null");
            }
            await playServices.JoinRoom(userId, roomId);


            return View();
            //TODO Зклепати
        }
        public async Task<IActionResult> ExitRoom()
        {
            string? Name = User?.Identity?.Name;
            await playServices.LeaveRoomAsync(Name);
            return RedirectToAction("Index");
        }
        public IActionResult No()
        {
            return RedirectToAction("Index","Home");
        }
    }
}
