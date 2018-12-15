using System.Threading.Tasks;
using TeamLegend.Models;

namespace TeamLegend.Services.Contracts
{
    public interface IStadiumsService
    {
        Task<Stadium> CreateAsync(Stadium stadium);

        Task<Stadium> GetByIdAsync(string id);
    }
}
