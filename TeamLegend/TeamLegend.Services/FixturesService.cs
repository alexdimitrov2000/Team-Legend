namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class FixturesService : IFixturesService
    {
        private readonly ApplicationDbContext context;

        public FixturesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Fixture> GetOrCreateAsync(int fixtureRound, string leagueId)
        {
            if (fixtureRound <= 0 || string.IsNullOrEmpty(leagueId))
                return null;

            var fixture = await this.context.Fixtures.FirstOrDefaultAsync(f => f.LeagueId == leagueId && f.FixtureRound == fixtureRound);

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
            if (string.IsNullOrEmpty(leagueId) || round <= 0)
                return null;

            var fixture = await this.context.Fixtures.FirstOrDefaultAsync(f => f.LeagueId == leagueId && f.FixtureRound == round);
            return fixture;
        }
    }
}
