namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;

    public interface IStadiumsService
    {
        Task<Stadium> CreateAsync(Stadium stadium);

        Task<Stadium> GetByIdAsync(string id);
    }
}
