using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamLegend.Models;
using TeamLegend.Web.Areas.Administration.Models.League;
using TeamLegend.Web.Models.Leagues;

namespace TeamLegend.Web.Mapping
{
    public class LeaguesProfile : Profile
    {
        public LeaguesProfile()
        {
            CreateMap<LeagueCreateInputModel, League>();

            CreateMap<League, LeagueDetailsViewModel>()
                .ForMember(l => l.NumberOfTeams,
                        opt => opt.MapFrom(src => src.Teams.Count()));

            CreateMap<League, LeagueIndexViewModel>()
                .ForMember(l => l.NumberOfTeams,
                        opt => opt.MapFrom(src => src.Teams.Count()));
        }
    }
}
