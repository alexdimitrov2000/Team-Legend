using AutoMapper;
using TeamLegend.Models;
using TeamLegend.Web.Areas.Administration.Models.Stadiums;

namespace TeamLegend.Web.Mapping
{
    public class StadiumsProfile : Profile
    {
        public StadiumsProfile()
        {
            CreateMap<StadiumCreateInputModel, Stadium>();
        }
    }
}
