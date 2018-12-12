using AutoMapper;
using TeamLegend.Models;
using TeamLegend.Web.Areas.Administration.Models.Stadiums;
using TeamLegend.Web.Models.Stadiums;

namespace TeamLegend.Web.Mapping
{
    public class StadiumsProfile : Profile
    {
        public StadiumsProfile()
        {
            CreateMap<StadiumCreateInputModel, Stadium>();

            CreateMap<Stadium, StadiumDetailsViewModel>();
        }
    }
}
