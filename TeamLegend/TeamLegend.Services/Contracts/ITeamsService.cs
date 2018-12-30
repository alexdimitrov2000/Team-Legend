namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ITeamsService
    {
        Task<Team> CreateAsync(Team team);

        Task<Team> GetByIdAsync(string id);

        Task<List<Team>> GetAllAsync();

        Task<List<Team>> GetAllWithoutLeagueAsync();

        Task<bool> DeleteAsync(Team team);

        Task<Team> SetStadiumAsync(Team team, Stadium stadium);

        Task<Team> AddNewPlayersAsync(Team team, List<Player> playersToAdd);

        Task<Team> AddManagerAsync(Team team, Manager manager);

        Task<Team> IncreasePlayersAppearancesAsync(Team team);
    }
}
