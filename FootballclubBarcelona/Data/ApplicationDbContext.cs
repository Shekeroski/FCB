using Microsoft.EntityFrameworkCore;
using FootballclubBarcelona.Models;

namespace FootballclubBarcelona.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketPurchasing> TicketPurchasings { get; set; }
        public DbSet<FootballclubBarcelona.Models.LeagueTable> LeagueTable { get; set; } = default!;
        public DbSet<FootballclubBarcelona.Models.TeamAndPlayers> TeamAndPlayers { get; set; } = default!;
        public DbSet<ProductPurchasing> ProductPurchasings { get; set; }
        
        
        
    }
}