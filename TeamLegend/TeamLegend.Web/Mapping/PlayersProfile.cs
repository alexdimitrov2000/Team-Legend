using AutoMapper;
using TeamLegend.Models;
using TeamLegend.Web.Areas.Administration.Models.Players;
using TeamLegend.Web.Models.Players;

namespace TeamLegend.Web.Mapping
{
    public class PlayersProfile : Profile
    {
        public PlayersProfile()
        {
            CreateMap<PlayerCreateInputModel, Player>();

            CreateMap<Player, PlayerDetailsViewModel>();
        }
    }
}
