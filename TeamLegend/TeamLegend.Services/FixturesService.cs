namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class FixturesService : IFixturesService
    {
        private readonly ApplicationDbContext context;

        public FixturesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Fixture> GetOrCreateAsync(int fixtureRound, string leagueId)
        {
            if (fixtureRound <= 0 || string.IsNullOrEmpty(leagueId) || string.IsNullOrWhiteSpace(leagueId))
                return null;

            var fixture = await this.context.Fixtures.SingleOrDefaultAsync(f => f.LeagueId == leagueId && f.FixtureRound == fixtureRound);

            if (fixture == null)
            {
                fixture = new Fixture { FixtureRound = fixtureRound, LeagueId = leagueId };
                await this.context.Fixtures.AddAsync(fixture);
                await this.context.SaveChangesAsync();
            }

            return fixture;
        }

        public async Task<Fixture> GetByLeagueIdAndRoundAsync(string leagueId, int round)
        {
            if (string.IsNullOrEmpty(leagueId) || string.IsNullOrWhiteSpace(leagueId))
                return null;

            var fixture = await this.context.Fixtures.SingleOrDefaultAsync(f => f.LeagueId == leagueId && f.FixtureRound == round);
            return fixture;
        }

        public async Task<int> ValidateRoundAsync(string leagueId, int round)
        {
            if (string.IsNullOrEmpty(leagueId) || string.IsNullOrWhiteSpace(leagueId))
                throw new ArgumentException("Invalid league id.");
            
            if (round <= 0)
                return 1;

            var lastRound = await this.context.Fixtures.Where(f => f.LeagueId == leagueId).OrderByDescending(f => f.FixtureRound).Select(f => f.FixtureRound).FirstOrDefaultAsync();

            if (round > lastRound)
                return lastRound;

            return round;
        }
    }
}
