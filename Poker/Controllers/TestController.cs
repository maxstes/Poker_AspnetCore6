using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poker.Models.Cards;
using Poker.Services;
using Poker.Models.Enums;
using Poker.Models.Cards.Enums;

namespace Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("/TestDeck")]
        public IActionResult Index()
        {
            List<Card> MyCards = new List<Card>();
            List<Card> OponentCards = new List<Card>();
            List<Card> TablesCards = new List<Card>();
            //List<Card> MyTablesCards = new List<Card>()
            //{ new Card(SuitEnum.Diamonds ,RankEnum.King),
            // new Card(SuitEnum.Diamonds ,RankEnum.Queen),
            //new Card(SuitEnum.Diamonds ,RankEnum.Jack),
            //new Card(SuitEnum.Diamonds,RankEnum.Ace),
            //new Card(SuitEnum.Diamonds,RankEnum.Ten;
            Deck deck = new Deck();
            deck.Shuffle();

            MyCards = deck.GetCards(2);
            OponentCards = deck.GetCards(2);
            TablesCards = deck.GetCards(5);

            List<Card> FullCards = new List<Card>();
            FullCards.AddRange(MyCards);
            FullCards.AddRange(TablesCards);

            CombinationSelection selection = new CombinationSelection(FullCards);
            selection.WriteCardsPlayers(FullCards, "My");
            FullCards.Clear();
            FullCards.AddRange(OponentCards); FullCards.AddRange(TablesCards);
            CombinationSelection selection1 = new CombinationSelection(FullCards);
            selection.WriteCardsPlayers(FullCards, "Enemy");
            Console.WriteLine($"My {selection.GetBestCombination()}");
            Console.WriteLine($"Oponent {selection1.GetBestCombination()}");
            return Ok();
        }
    }
}
