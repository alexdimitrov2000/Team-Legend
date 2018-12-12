namespace TeamLegend.Services
{
    using System.Threading.Tasks;
    using Contracts;
    using TeamLegend.Data;
    using TeamLegend.Models;

    public class StadiumsService : IStadiumsService
    {
        private readonly ApplicationDbContext context;

        public StadiumsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(string name, string location, double capacity, string imageVersion)
        {
            var stadium = new Stadium
            {
                Name = name,
                Location = location,
                Capacity = capacity,
                StadiumPictureVersion = imageVersion
            };

            await this.context.Stadiums.AddAsync(stadium);
            await this.context.SaveChangesAsync();
        }
    }
}
