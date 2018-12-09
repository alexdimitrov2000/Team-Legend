using System;
using System.Threading.Tasks;
using TeamLegend.Models;

namespace TeamLegend.Services.Contracts
{
    public interface IPlayersService
    {
        Task CreateAsync(Player player);

        Task<Player> GetByIdAsync(string id);
    }
}
