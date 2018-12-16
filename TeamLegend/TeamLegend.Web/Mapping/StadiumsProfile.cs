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
        }
    }
}
