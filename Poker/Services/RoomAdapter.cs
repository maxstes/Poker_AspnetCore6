using Microsoft.EntityFrameworkCore;
using Poker.Data;
using Poker.Models.Game;
using System.Security.Cryptography.X509Certificates;

namespace Poker.Services
{
    public class RoomAdapter
    {
        readonly ApplicationDbContext _context = new ();
        public async Task<List<Room>> GetRoomsAsync()
        {
            List<Room> Rooms = await _context.Room.ToListAsync();
            return Rooms;
        }
        public async Task<bool> CreatedRoom(Room room)
        {
             var result = _context.Room.AddAsync(room);
            if (result.IsCompletedSuccessfully)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
        public async Task<Room> GetRoom(int idRoom)
        {
            var result = await _context.Room
                .Where(r => r.Id == idRoom)
                .FirstOrDefaultAsync();
            
            return result;
        }
        public async Task AddPlayer(Player players)
        {
            await _context.Player.AddAsync(players);
            await _context.SaveChangesAsync();
        }
        
    }
}
