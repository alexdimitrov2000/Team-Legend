namespace TeamLegend.Services
{
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeamsService : ITeamsService
    {
        private readonly ApplicationDbContext context;

        public TeamsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Team> CreateAsync(Team team)
        {
            if (team == null)
                return null;

            await this.context.Teams.AddAsync(team);
            await this.context.SaveChangesAsync();

            return team;
        }

        public async Task<Team> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return await this.context.Teams.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Team> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return await this.context.Teams.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await this.context.Teams.ToListAsync();
        }

        public async Task<List<Team>> GetAllWithoutLeagueAsync()
        {
            return await this.context.Teams.Where(t => t.LeagueId == null).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Team team)
        {
            if (team == null)
                return false;

            this.context.Teams.Remove(team);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Team> SetStadiumAsync(Team team, Stadium stadium)
        {
            if (team == null || stadium == null)
                return null;

            team.Stadium = stadium;
            this.context.Teams.Update(team);
            await this.context.SaveChangesAsync();

            return team;
        }

        public async Task<Team> AddNewPlayersAsync(Team team, List<Player> playersToAdd)
        {
            if (team == null || playersToAdd == null)
                return null;

            playersToAdd.ForEach(p => team.Players.Add(p));
            this.context.Teams.Update(team);

            await this.context.SaveChangesAsync();

            return team;
        }

        public async Task<Team> AddManagerAsync(Team team, Manager manager)
        {
            if (team == null || manager == null)
                return null;

            if (team.Manager != null)
                team.Manager.Team = null;

            team.Manager = manager;
            this.context.Teams.Update(team);

            await this.context.SaveChangesAsync();

            return team;
        }

        public async Task<Team> IncreasePlayersAppearancesAsync(Team team)
        {
            if (team == null)
                return null;

            var players = team.Players.ToList();
            players.ForEach(p => p.Appearances++);

            this.context.Players.UpdateRange(players);
            await this.context.SaveChangesAsync();

            return team;
        }
    }
}
