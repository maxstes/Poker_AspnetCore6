using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poker.Models.Cards;
using Poker.Services;

namespace Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("/TestDeck")]
        public void Index()
        {
            Deck deck = new Deck();
            deck.Shuffle();

            for(int i = 0;i < 15;i++)
            {
                Card card = deck.DealCard();
                Console.WriteLine("card issued:" + card);
            }
        }
        
    }
}
