namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class MatchesService : IMatchesService
    {
        private readonly ApplicationDbContext context;

        public MatchesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Match> CreateAsync(Match match)
        {
            if (match == null)
                return null;

            await this.context.Matches.AddAsync(match);
            await this.context.SaveChangesAsync();

            return match;
        }

        public async Task<List<Match>> GetAllPlayedAsync()
        {
            return await this.context.Matches.Where(m => m.IsPlayed == true).ToListAsync();
        }

        public async Task<List<Match>> GetAllUnplayedAsync()
        {
            return await this.context.Matches.Where(m => m.IsPlayed == false).ToListAsync();
        }

        public async Task<Match> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var match = await this.context.Matches.FirstOrDefaultAsync(m => m.Id == id);

            return match;
        }

        public async Task<Match> UpdateScoreAsync(Match match, int homeTeamGoals, int awayTeamGoals)
        {
            if (homeTeamGoals <= 0 || awayTeamGoals <= 0 || match == null)
                return null;

            match.HomeTeamGoals = homeTeamGoals;
            match.AwayTeamGoals = awayTeamGoals;
            match.IsPlayed = true;

            this.context.Matches.Update(match);
            await this.context.SaveChangesAsync();

            return match;
        }
    }
}
