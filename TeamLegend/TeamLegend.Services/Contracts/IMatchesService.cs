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
    }
}
