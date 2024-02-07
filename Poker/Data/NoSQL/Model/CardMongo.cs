using MongoDB.Bson;
using Poker.Models.Cards;

namespace Poker.Data.NoSQL.Model
{
    public class CardMongo
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<string> Cards { get; set; } = new List<string>();
    }
}
