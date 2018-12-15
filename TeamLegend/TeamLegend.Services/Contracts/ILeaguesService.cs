using System.Collections.Generic;
using System.Threading.Tasks;
using TeamLegend.Models;

namespace TeamLegend.Services.Contracts
{
    public interface ILeaguesService
    {
        Task<League> CreateAsync(string name, string countryName);

        Task<League> GetByIdAsync(string id);

        Task<ICollection<League>> GetAllAsync();
    }
}
