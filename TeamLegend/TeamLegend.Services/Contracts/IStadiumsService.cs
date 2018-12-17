namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IStadiumsService
    {
        Task<Stadium> CreateAsync(Stadium stadium);

        Task<Stadium> GetByIdAsync(string id);

        Task<bool> DeleteAsync(Stadium stadium);

        IQueryable<Stadium> GetAll();
    }
}
