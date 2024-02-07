using Poker.Models.Game;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Poker.Data.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public int Bank { get; set; }

        public List<Player> Players { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
