namespace TeamLegend.Services.Contracts
{
    using TeamLegend.Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IManagersService
    {
        Task<Manager> CreateAsync(Manager manager);

        Task<Manager> GetByIdAsync(string id);

        Task<List<Manager>> GetAllAsync();

        Task<bool> DeleteAsync(Manager manager);
    }
}
