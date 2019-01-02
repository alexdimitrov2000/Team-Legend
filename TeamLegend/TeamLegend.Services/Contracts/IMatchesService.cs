namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IMatchesService
    {
        Task<Match> CreateAsync(Match match);

        Task<List<Match>> GetAllPlayedAsync();

        Task<List<Match>> GetAllUnplayedAsync();

        Task<Match> GetByIdAsync(string id);

        Task<Match> UpdateScoreAsync(Match match, int homeTeamGoals, int awayTeamGoals);

        Task<Match> UpdateTeamsGoalsAsync(Match match, int homeTeamGoals, int awayTeamGoals);

        Task<bool> DeleteTeamMatches(string teamId);
    }
}
