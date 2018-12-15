namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

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
    }
}
