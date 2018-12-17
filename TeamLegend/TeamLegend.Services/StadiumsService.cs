namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;

    public class StadiumsService : IStadiumsService
    {
        private readonly ApplicationDbContext context;

        public StadiumsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Stadium> CreateAsync(Stadium stadium)
        {
            await this.context.Stadiums.AddAsync(stadium);
            await this.context.SaveChangesAsync();

            return stadium;
        }

        public async Task<Stadium> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return await this.context.Stadiums.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> DeleteAsync(Stadium stadium)
        {
            this.context.Stadiums.Remove(stadium);
            await this.context.SaveChangesAsync();
            return true;
        }

        public IQueryable<Stadium> GetAll()
        {
            return this.context.Stadiums.AsQueryable();
        }
    }
}
