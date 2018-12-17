namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IPlayersService
    {
        Task<Player> CreateAsync(Player player);

        Task<Player> GetByIdAsync(string id);

        Task<Player> GetByNameAsync(string name);

        Task<bool> DeleteAsync(Player player);

        Task<ICollection<Player>> GetAllAsync();

        Task<ICollection<Player>> GetAllWithoutTeamAsync();

        Task<Player> AddPlayerToTeamAsync(Player player, Team team);
    }
}
