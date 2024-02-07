namespace Poker.Data.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public string Card { get; set; } = null!;
        public int TablesCardEntityId { get; set; }
        public TablesCardEntity TablesCardEntity { get; set; }
    }
}
