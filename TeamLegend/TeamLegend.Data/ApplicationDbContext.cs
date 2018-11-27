namespace TeamLegend.Data
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using TeamLegend.Data.EntityConfiguration;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LeagueConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new StadiumConfiguration());
            builder.ApplyConfiguration(new FixtureConfiguration());
            builder.ApplyConfiguration(new MatchConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
