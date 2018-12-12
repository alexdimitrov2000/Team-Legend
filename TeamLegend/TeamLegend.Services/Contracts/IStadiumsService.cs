using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamLegend.Services.Contracts
{
    public interface IStadiumsService
    {
        Task CreateAsync(string name, string location, double capacity, string imageVersion);
    }
}
