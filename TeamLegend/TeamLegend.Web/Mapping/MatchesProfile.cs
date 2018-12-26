namespace TeamLegend.Web.Mapping
{
    using Models.Home;
    using TeamLegend.Models;
    using Areas.Administration.Models.Matches;

    using AutoMapper;

    public class MatchesProfile : Profile
    {
        public MatchesProfile()
        {
            CreateMap<MatchCreateInputModel, Match>();

            CreateMap<Match, MatchHomeViewModel>()
                .ForMember(m => m.StadiumName,
                        opt => opt.MapFrom(src => src.HomeTeam.Stadium.Name));

            CreateMap<Match, MatchFinalizeViewModel>();
        }
    }
}
