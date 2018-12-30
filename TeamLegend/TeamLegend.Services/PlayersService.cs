namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class PlayersService : IPlayersService
    {
        private readonly ApplicationDbContext context;

        public PlayersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Player> CreateAsync(Player player)
        {
            if (player == null)
                return null;

            await this.context.Players.AddAsync(player);
            await this.context.SaveChangesAsync();

            return player;
        }

        public async Task<Player> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                return null;

            return await this.context.Players.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> DeleteAsync(Player player)
        {
            if (player == null)
                return false;

            this.context.Players.Remove(player);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Player>> GetAllAsync()
        {
            return await this.context.Players.ToListAsync();
        }

        public async Task<ICollection<Player>> GetAllWithoutTeamAsync()
        {
            return await this.context.Players.Where(p => p.CurrentTeam == null && p.CurrentTeamId == null).ToListAsync();
        }

        public async Task<ICollection<Player>> GetAllFromTeamAsync(string teamId)
        {
            if (string.IsNullOrEmpty(teamId) || string.IsNullOrWhiteSpace(teamId))
                return null;

            return await this.context.Players.Where(p => p.CurrentTeamId == teamId).ToListAsync();
        }

        public async Task<List<Player>> IncreasePlayersGoalsAsync(List<Player> players)
        {
            if (players == null)
                return null;

            players.ForEach(p => p.GoalsScored++);

            this.context.Players.UpdateRange(players);
            await this.context.SaveChangesAsync();

            return players;
        }
    }
}
