using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Poker.Data.NoSQL.Model;
using Poker.Models.Cards;
using Poker.Services;
using System;

namespace Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : Controller
    {
        readonly IMongoDatabase client;
        readonly IMongoCollection<BsonDocument> collectionUsers;
        readonly Deck deck = new ();
        public MongoController([FromServices] MongoClient mongoClient)
        {
            client = mongoClient.GetDatabase("test");
            collectionUsers = client.GetCollection<BsonDocument>("users");
        }
        public async Task AddAsync(string value)
        {
            var collection = client.GetCollection<BsonDocument>("users");
            await collection.InsertOneAsync(new BsonDocument { { "name", $"{value}" }, });

        }
        [HttpGet("/MongoDes")]
        public void TestLanguage()
        {
            BsonDocument doc = new ()
            {
                {"Name","maxstes"},
                {"Cards", new BsonArray{"english", "german", "spanish"} }
            };
            CardMongo person = BsonSerializer.Deserialize<CardMongo>(doc);
            Console.WriteLine(person.ToJson());
        }

        [HttpGet("/addmongo")]
        public async Task<IActionResult> Index()
        {

            var collection = client.GetCollection<BsonDocument>("cards");
            string username = "maxstes";
            List<Card> cards = deck.GetPlayersCard();

            List<string> strings = new();
            foreach (var x in cards)
            {
                strings.Add(x.ToString());
            }

            BsonDocument doc = new ()
                {
                    {"Name",username},
                    {"Cards", new BsonArray(strings) }
                };


            await collection.InsertOneAsync(doc);


            return Ok();
        }
        [HttpGet("/GetCards")]
        public async Task<IActionResult> GetCardsAsync()
        {
            var collection = client.GetCollection<BsonDocument>("cards");

            var user = await collection.Find(new BsonDocument { { "Name", "maxstes" } }).FirstOrDefaultAsync();

            CardMongo card = BsonSerializer.Deserialize<CardMongo>(user);
            foreach (var cardOne in card.Cards)
            {
                await Console.Out.WriteLineAsync($"{card.Name},{cardOne}s");
            }
            Console.WriteLine($"{card.Name},{card.Cards}");//по нормальному вывести
            //person = BsonSerializer.Deserialize<Person>(doc);
            // var result = collection.FindAsync(FilterBson).Result.ToString();
            //  var x = GetCardsCollection(result);
            // Console.WriteLine(result);            
            //  foreach (var card in x)
            // {
            //     await Console.Out.WriteLineAsync($"{card}");
            //  }
            return Ok();
        }
        [HttpGet("/GetAll")]
        public async Task GetAll()
        {
            try
            {
                var collection = client.GetCollection<BsonDocument>("cards");
                List<BsonDocument> users = await collection.Find(new BsonDocument()).ToListAsync();
                await Console.Out.WriteLineAsync("Successfully connected to MongoDB");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error connection to MongoDb {ex.Message}");
            }
        }

        public List<string> GetCardsCollection(string cards)
        {
            var Cards = cards.Split(',').ToList();
            return Cards;
        }
        public string GetStringCollection(List<Card> cards)
        {
            string result = string.Join(",", cards);
            return result;
        }
    }

}
