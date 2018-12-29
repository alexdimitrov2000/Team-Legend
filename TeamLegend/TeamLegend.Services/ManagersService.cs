namespace TeamLegend.Services
{
    using Data;
    using Contracts;
    using TeamLegend.Models;

    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class ManagersService : IManagersService
    {
        private readonly ApplicationDbContext context;

        public ManagersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Manager> CreateAsync(Manager manager)
        {
            if (manager == null)
                return null;

            await this.context.Managers.AddAsync(manager);
            await this.context.SaveChangesAsync();

            return manager;
        }

        public async Task<Manager> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            return await this.context.Managers.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Manager>> GetAllAsync()
        {
            return await this.context.Managers.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Manager manager)
        {
            if (manager == null)
                return false;

            this.context.Managers.Remove(manager);
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
