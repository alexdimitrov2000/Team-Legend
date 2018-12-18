namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ILeaguesService
    {
        Task<League> CreateAsync(League league);

        Task<League> AddTeamsAsync(League league, List<Team> teams);

        Task<League> GetByIdAsync(string id);

        Task<ICollection<League>> GetAllAsync();

        Task<bool> DeleteAsync(League league);
    }
}
