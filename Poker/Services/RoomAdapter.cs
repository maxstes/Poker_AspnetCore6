using Microsoft.EntityFrameworkCore;
using Poker.Data;
using Poker.Models.Game;

namespace Poker.Services
{
    public class RoomAdapter
    {
        readonly ApplicationDbContext _context;
        public RoomAdapter()
        {
            _context = new ApplicationDbContext();
        }
        public async Task<List<Room>> GetRooms()
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

    }
}
