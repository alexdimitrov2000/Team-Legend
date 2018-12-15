namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Collections.Generic;

    public class LeaguesService : ILeaguesService
    {
        private readonly ApplicationDbContext context;

        public LeaguesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<League> CreateAsync(string name, string countryName)
        {
            var league = new League
            {
                Name = name,
                Country = countryName
            };

            await this.context.Leagues.AddAsync(league);
            await this.context.SaveChangesAsync();

            return league;
        }

        public async Task<ICollection<League>> GetAllAsync()
        {
            return await this.context.Leagues.ToListAsync();
        }

        public async Task<League> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return await this.context.Leagues.SingleOrDefaultAsync(l => l.Id == id);
        }
    }
}
