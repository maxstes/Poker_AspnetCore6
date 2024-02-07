using Poker.Models.Cards;
using Poker.Models.Game;
using Poker.Services.Interface;
using Poker.Models.Cards.Enums;

namespace Poker.Services
{
    public class FinishService
    {
        readonly ILogger<FinishService> logger;
        readonly CookiesService cookiesService;
        readonly Deck deck;
        readonly CombinationSelection selection;
        public FinishService(ILogger<FinishService> logger, CookiesService cookiesService, Deck deck,CombinationSelection selection)
        {
            this.logger = logger;
            this.cookiesService = cookiesService;
            this.deck = deck;
            this.selection = selection;
        }
        public void Finish()
        {
            deck.Shuffle();
            //  For Tests
            //List < Card > cardsList = new() 
            //{ new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.Ten } ,
            //  new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.Jack },
            // // new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.Queen },
            // // new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.King },
            // // new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.Ace }
            //};
            //List<Card> tableListList = new()
            //{ new Card {Suit= SuitEnum.Spades,Value= RankEnum.Six } ,
            //  new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.Eight},
            //  new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.Seven },
            //  new Card {Suit= SuitEnum.Diamonds,Value= RankEnum.Four },
            //  new Card {Suit= SuitEnum.Spades,Value= RankEnum.King }
            //};
           // deck.DealCard(cardsList);
            //deck.DealCard(tableListList);
            var TablesCards = deck.GetTableCard();

            var MyCards = deck.GetPlayersCard();
            var OponentCards = deck.GetPlayersCard();

            int MyId = 1, EnemyId = 2;

            CombinationUser combinationMy = new(MyId, MyCards, TablesCards);

            CombinationUser combinationEnemy = new(EnemyId, OponentCards, TablesCards);

            CombinationUser[] combinationUsersMas = new[] { combinationMy, combinationEnemy };

            var MasResult = selection.GetBestCombination(combinationUsersMas);

             selection.GetWinner(MasResult);
        }
    }
}
