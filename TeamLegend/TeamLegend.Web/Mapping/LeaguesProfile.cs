namespace TeamLegend.Web.Mapping
{
    using Models.Leagues;
    using TeamLegend.Models;
    using Areas.Administration.Models.League;
    using Areas.Administration.Models.Leagues;

    using AutoMapper;

    using System.Linq;

    public class LeaguesProfile : Profile
    {
        public LeaguesProfile()
        {
            CreateMap<LeagueCreateInputModel, League>();

            CreateMap<League, LeagueDetailsViewModel>();

            CreateMap<League, LeagueIndexViewModel>()
                .ForMember(l => l.NumberOfTeams,
                        opt => opt.MapFrom(src => src.Teams.Count()));

            CreateMap<League, LeagueDeleteViewModel>()
                .ForMember(m => m.NumberOfTeams,
                        opt => opt.MapFrom(src => src.Teams.Count));
        }
    }
}
