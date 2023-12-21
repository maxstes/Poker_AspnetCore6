using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Poker.Models;
using Poker.Models.Game;

namespace Poker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        { 
           
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
            
        {
           // Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-HIR5786\\SQLEXPRESS;Database=PokerDb;User Id = sa;Password=Zabiyaka1337;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
        public DbSet<Room> Room { get; set; } = null!;
        public DbSet<Player> Player { get; set; } = null!;
        public DbSet<PlayerOnline> PlayerOnline { get; set; } = null!;
    }
}