namespace TeamLegend.Web.Mapping
{
    using Models.Players;
    using TeamLegend.Models;
    using Areas.Administration.Models.Players;

    using AutoMapper;

    public class PlayersProfile : Profile
    {
        public PlayersProfile()
        {
            CreateMap<PlayerCreateInputModel, Player>()
                .ForMember(m => m.CurrentTeamId,
                        opt => opt.MapFrom(src => src.TeamId));

            CreateMap<Player, PlayerDetailsViewModel>();

            CreateMap<Player, PlayerDeleteViewModel>();

            CreateMap<Player, PlayerViewModel>();
        }
    }
}
