namespace TeamLegend.Web.Mapping
{
    using Models.Teams;
    using TeamLegend.Models;
    using Areas.Administration.Models.Teams;

    using AutoMapper;

    public class TeamsProfile : Profile
    {
        public TeamsProfile()
        {
            CreateMap<TeamCreateInputModel, Team>();

            CreateMap<Team, TeamDetailsViewModel>()
                .ForMember(m => m.YearOfFoundation,
                        opt => opt.MapFrom(src => src.DateOfFoundation.GetValueOrDefault().Year));
        }
    }
}
