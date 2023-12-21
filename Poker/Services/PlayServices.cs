using Poker.Data;
using Poker.Models.Game;
using System.Numerics;

namespace Poker.Services
{
    public class PlayServices
    {
        private readonly ApplicationDbContext _context;
        public PlayServices()
        {
            _context = new ApplicationDbContext();
        }
        public void JoinRoom(int PlayerId, int roomId)
        { 
            var PlayerOnline = GetPlayersOnline(PlayerId, roomId);

            _context.PlayerOnline.Add(PlayerOnline);
            _context.SaveChanges();
        }
        public void LeaveRoom(int playerId, int roomId)
        {
            var PlayerOnline = GetPlayersOnline(playerId, roomId);

            _context.PlayerOnline.Remove(PlayerOnline);
        }
        // public PlayersOnline GetPlayersOnline(PlayersOnline playersOnline)
        //{
        //    var Player = GetPlayers(playersOnline.Id);
        //    var Room = GetRoom(playersOnline.RoomId);

        //}
        public PlayerOnline GetPlayersOnline (int playerId, int roomId)
        {
            var Player = GetPlayers(playerId);
            var Room = GetRoom(roomId);

            var PlayerOnline =  new PlayerOnline {Player = Player,Room = Room,PlayerId=Player.Id,RoomId = Room.Id };

            return  PlayerOnline;
        }
        private Player GetPlayers(int id)
        {
            var Player = _context.Player
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return Player;
        }
        private Room GetRoom(int roomId)
        {
            var Room = _context.Room
                .Where(x => x.Id == roomId)
                .FirstOrDefault();
            return Room;
        }
    }
}

