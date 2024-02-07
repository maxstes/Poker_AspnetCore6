using Microsoft.AspNetCore.Mvc;
using Poker.Models.Game;
using Poker.Services;

namespace Poker.Controllers
{
    public class GameController : Controller
    {
        readonly PlayServices _playServices;
        readonly RoomAdapter _roomAdapter;
        public GameController(PlayServices playServices,RoomAdapter roomAdapter)
        {
            _playServices = playServices;
            _roomAdapter = roomAdapter;
        }
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
            var model = _roomAdapter.GetRoomsAsync().Result;
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
            string? UserName = User.Identity?.Name;
            var PlayersOnline = _playServices.GetPlayersInRoom(UserName, idRoom).Result;
            //TODO create list players (collection) to view
            return View(PlayersOnline); 
        }
    }
}
