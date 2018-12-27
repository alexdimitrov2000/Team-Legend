namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;
    
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class StadiumsService : IStadiumsService
    {
        private readonly ApplicationDbContext context;

        public StadiumsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Stadium> CreateAsync(Stadium stadium)
        {
            if (stadium == null)
                return null;

            await this.context.Stadiums.AddAsync(stadium);
            await this.context.SaveChangesAsync();

            return stadium;
        }

        public async Task<Stadium> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return await this.context.Stadiums.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> DeleteAsync(Stadium stadium)
        {
            if (stadium == null)
                return false;

            this.context.Stadiums.Remove(stadium);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Stadium>> GetAllAsync()
        {
            return await this.context.Stadiums.ToListAsync();
        }
    }
}
