using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poker.Models.Cards;
using Poker.Services;
using Poker.Models.Enums;
using Poker.Models.Cards.Enums;
using Poker.Models.Game;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Poker.Controllers
{
    public class TestController : Controller
    {
        readonly CombinationSelection selection = new();
        readonly CookiesService cookiesService;
        readonly Deck deck = new ();
        readonly FinishService finish;

        public TestController(CookiesService _cookiesService, FinishService finish)
        {
            cookiesService = _cookiesService;
            this.finish = finish;
        }
        public IActionResult GetUserCard()
        {
            string cookiesKeyCards = "1";
            var session = HttpContext.Session;
            var firstplayer = deck.GetPlayersCard();
            cookiesService.AddCookiesCards(cookiesKeyCards, firstplayer, session);
            var Card = cookiesService.GetCookiesCards(cookiesKeyCards, session);
            return Ok(Card);
        }
        public void CardsCookiesTest()
        {
            var session = HttpContext.Session;
            deck.Shuffle();
            var secondplayer = deck.GetPlayersCard();
            var threesplayer = deck.GetPlayersCard();
            var table = deck.GetTableCard();

            cookiesService.AddCookiesCards("2", secondplayer, session);
            cookiesService.AddCookiesCards("3", threesplayer, session);
            cookiesService.AddCookiesCards("4", table, session);

            var mas1 = cookiesService.GetCookiesCards("1", session);
            var mas2 = cookiesService.GetCookiesCards("2", session);
            var mas3 = cookiesService.GetCookiesCards("4",session);

            var list = new List<Card>();
            
            list.AddRange(mas1);list.AddRange(mas2);list.AddRange(mas3);
            
            foreach(var m in list)
            {
                Console.WriteLine(m.ToString());
            }

           // return View();
        }


        [HttpGet("/TestDeck")]
        public IActionResult Room(int RoomId,int PlayerId)
        {

            return View();
        }
        public void Final()
        {
            finish.Finish();
        }
    }
}
