using System.Threading.Tasks;
using TeamLegend.Models;

namespace TeamLegend.Services.Contracts
{
    public interface IStadiumsService
    {
        Task CreateAsync(Stadium stadium);
    }
}
