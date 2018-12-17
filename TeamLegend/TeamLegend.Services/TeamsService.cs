namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;

    public class TeamsService : ITeamsService
    {
        private readonly ApplicationDbContext context;

        public TeamsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Team> CreateAsync(Team team)
        {
            await this.context.Teams.AddAsync(team);
            await this.context.SaveChangesAsync();

            return team;
        }

        public async Task<Team> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return await this.context.Teams.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Team> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return await this.context.Teams.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Team> UpdateAsync(Team team)
        {
            this.context.Teams.Update(team);
            await this.context.SaveChangesAsync();
            return team;
        }

        public async Task<bool> DeleteAsync(Team team)
        {
            this.context.Teams.Remove(team);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Team> SetStadiumAsync(Team team, Stadium stadium)
        {
            team.Stadium = stadium;
            this.context.Teams.Update(team);
            await this.context.SaveChangesAsync();

            return team;
        }

        public async Task<Team> AddNewPlayersAsync(Team team, List<Player> playersToAdd)
        {
            playersToAdd.ForEach(p => team.Players.Add(p));
            this.context.Teams.Update(team);

            await this.context.SaveChangesAsync();

            return team;
        }
    }
}
