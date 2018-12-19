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

            CreateMap<Team, TeamEditViewModel>();

            CreateMap<TeamEditViewModel, Team>();

            CreateMap<Team, TeamDeleteViewModel>()
                .ForMember(m => m.StadiumName,
                        opt => opt.MapFrom(src => src.Stadium == null ? "Does not have a stadium." : src.Stadium.Name))
                .ForMember(m => m.ManagerName,
                        opt => opt.MapFrom(src => src.Manager == null ? "Does not have a manager." : src.Manager.Name))
                .ForMember(m => m.LeagueName,
                        opt => opt.MapFrom(src => src.League == null ? "Does not participate in any league." : src.League.Name));

            CreateMap<Team, TeamViewModel>()
                .ForMember(m => m.MatchesPlayed,
                        opt => opt.MapFrom(src => src.Matches.Count));
        }
    }
}
