using Microsoft.AspNetCore.Mvc;
using Poker.Models.Game;
using Poker.Services;

namespace Poker.Controllers
{
    public class GameController : Controller
    {
        readonly RoomAdapter _roomAdapter = new ();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/newRoom")]
        public IActionResult CreateRoom() => View();
        [HttpGet("/Rooms")]
        public IActionResult Rooms()
        {
            var model = _roomAdapter.GetRooms().Result;
            return View(model);
        }

        [HttpPost("/newRoom")]
        public async Task<IActionResult> CreateRoom(Room room)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            room.TimeCreatedRoom = DateTime.UtcNow;
            var x = await _roomAdapter.CreatedRoom(room);
            if(x == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("/Rooms")]
        public IActionResult Rooms(RoomParam param)
        {
            
            return View();
        }
        [HttpGet("/Room")]
        public IActionResult Room(int idRoom)
        {
            var room = _roomAdapter.GetRoom(idRoom).Result;
            return View(room); 
        }
    }
}
