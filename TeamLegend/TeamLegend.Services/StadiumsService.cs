namespace TeamLegend.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using TeamLegend.Data;
    using TeamLegend.Models;

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
    }
}
