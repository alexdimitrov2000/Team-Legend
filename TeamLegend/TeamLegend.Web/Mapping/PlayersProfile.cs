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
            CreateMap<PlayerCreateInputModel, Player>();

            CreateMap<Player, PlayerDetailsViewModel>();
        }
    }
}
