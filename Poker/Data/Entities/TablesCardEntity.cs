namespace Poker.Data.Entities
{
    public class TablesCardEntity
    {
        public int Id { get; set; }
        public List<CardEntity> Cards { get; set; }
        public int RoomId { get; set; }
    }
}
