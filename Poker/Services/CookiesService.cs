using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Poker.Controllers;
using Poker.Models.Cards;
using System.Text.Json;

namespace Poker.Services
{
    public class CookiesService
    {
        readonly ILogger<CookiesService> Logger;
        public CookiesService(ILogger<CookiesService> logger)
        {
            this.Logger = logger;
        }

        public List<Card> GetCookiesCards(string key, ISession session)
        {
            var cards = session.GetString(key);
            return GetCardsCollection1(cards!);
        }
        public void AddCookiesCards(string key, List<Card> value, ISession session)
        {
            string cards = GetStringCollection1(value);
            AddCookies(key, cards, session);
        }
        public void RemoveCardsCookies(string key, ISession session)
        {
            session.Remove(key);
        }
        public void AddCookies(string nameCookie, string value, ISession session)
        {
            if (IsNullCookies(nameCookie, session))
            {
                session.SetString(nameCookie, value);
            }
            // logger.LogInformation($"Session Name {nameCookie}: {value}");
        }
        public void AddCookiesEmail(string email, ISession context)
        {
            const string SessionKeyEmail = "Email";

            AddCookies(SessionKeyEmail, email, context);

        }
        private bool IsNullCookies(string key, ISession session)
        {

            if (string.IsNullOrEmpty(session.GetString(key)))
            {
                return true;

            }
            return false;
        }
        //TODO Parse on OrParse method add to base class with MongoController*
        private List<Card> GetCardsCollection1(string jsonString)
        {
            // Повернення назад в List<Card>
            List<Card>? deserializedCards = JsonSerializer.Deserialize<List<Card>>(jsonString);

            // Вивід результата десеріалізації
           // Logger.LogInformation("\nDeserialized string back to List<Card>:");
            foreach (var card in deserializedCards!)
            {
              //  Logger.LogInformation($"Suit: {card.Suit}, Rank: {card.Value}");
            }
            return deserializedCards;
        }
        private string GetStringCollection1(List<Card> cards)
        {
            string jsonString = JsonSerializer.Serialize(cards);

            // Вивід рядка
           // Logger.LogInformation("Serialized List<Card> to string:");
           // Logger.LogInformation(jsonString);
            return jsonString;

        }

    }
}
