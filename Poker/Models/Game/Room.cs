using Poker.Models.Game;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poker.Models.Game;

public class Room
{
   //TODO забабахать подписи
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MinBet { get; set; } = 1;
    public int MaxBet { get; set; } = 1000;
    public int PlayersCount { get; set; }
    public List<PlayerOnline>? Players { get; set; }
    public int? MaxPlayers { get; set; } = 10;
    public int? FreeMisc { get; set; }
    public DateTime? TimeCreatedRoom { get; set; }
}
