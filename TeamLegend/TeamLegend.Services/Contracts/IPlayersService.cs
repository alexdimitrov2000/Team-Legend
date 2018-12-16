namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;

    public interface IPlayersService
    {
        Task<Player> CreateAsync(Player player);

        Task<Player> GetByIdAsync(string id);

        Task<bool> DeleteAsync(Player player);
    }
}
