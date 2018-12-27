namespace TeamLegend.Web.Mapping
{
    using Models.Stadiums;
    using TeamLegend.Models;
    using Areas.Administration.Models.Stadiums;

    using AutoMapper;

    public class StadiumsProfile : Profile
    {
        public StadiumsProfile()
        {
            CreateMap<StadiumCreateInputModel, Stadium>();

            CreateMap<Stadium, StadiumDetailsViewModel>();

            CreateMap<Stadium, StadiumDeleteViewModel>();

            CreateMap<Stadium, Areas.Administration.Models.Stadiums.StadiumViewModel>();

            CreateMap<Stadium, Models.Stadiums.StadiumViewModel>();
        }
    }
}
