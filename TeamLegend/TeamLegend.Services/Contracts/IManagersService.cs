namespace TeamLegend.Services.Contracts
{
    using TeamLegend.Models;

    using System.Threading.Tasks;

    public interface IManagersService
    {
        Task<Manager> CreateAsync(Manager manager);

        Task<Manager> GetByIdAsync(string id);
    }
}
