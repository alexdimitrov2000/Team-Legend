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

        public async Task<Fixture> GetOrCreateAsync(int fixtureRound)
        {
            if (fixtureRound <= 0)
                return null;

            var fixture = await this.context.Fixtures.FirstOrDefaultAsync(f => f.FixtureRound == fixtureRound);

            if (fixture == null)
            {
                fixture = new Fixture { FixtureRound = fixtureRound };
                await this.context.Fixtures.AddAsync(fixture);
                await this.context.SaveChangesAsync();
            }

            return fixture;
        }
    }
}
