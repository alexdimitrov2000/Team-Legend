namespace TeamLegend.Web.Mapping
{
    using TeamLegend.Models;
    using Areas.Administration.Models.Matches;

    using AutoMapper;

    public class MatchesProfile : Profile
    {
        public MatchesProfile()
        {
            CreateMap<MatchCreateInputModel, Match>();
        }
    }
}
