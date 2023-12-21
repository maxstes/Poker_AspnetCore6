namespace Poker.Models.Game
{
    public static class Move
    {
        public static string? Fold { get; set; } = "Fold";
        public static string? Check { get; set; } = "Check";
        public static string? Bet { get; set; } = "Bet";
        public static string? Call { get; set; } = "Call";
        public static string? Rais { get; set; } = "Rais";
    }
}
