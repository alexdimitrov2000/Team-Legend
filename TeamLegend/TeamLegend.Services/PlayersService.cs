namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;
    
    using System.Threading.Tasks;

    public class PlayersService : IPlayersService
    {
        private readonly ApplicationDbContext context;

        public PlayersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Player> CreateAsync(Player player)
        {
            await this.context.Players.AddAsync(player);
            await this.context.SaveChangesAsync();

            return player;
        }

        public async Task<Player> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return await this.context.Players.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> DeleteAsync(Player player)
        {
            this.context.Players.Remove(player);
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
