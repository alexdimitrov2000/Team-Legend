namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IStadiumsService
    {
        Task<Stadium> CreateAsync(Stadium stadium);

        Task<Stadium> GetByIdAsync(string id);

        Task<bool> DeleteAsync(Stadium stadium);

        Task<List<Stadium>> GetAllAsync();
    }
}
