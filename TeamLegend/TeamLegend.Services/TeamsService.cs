namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;

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
    }
}
