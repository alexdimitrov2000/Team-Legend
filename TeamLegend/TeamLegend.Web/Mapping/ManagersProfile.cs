namespace TeamLegend.Web.Mapping
{
    using Models.Managers;
    using TeamLegend.Models;
    using Areas.Administration.Models.Managers;

    using AutoMapper;

    public class ManagersProfile : Profile
    {
        public ManagersProfile()
        {
            CreateMap<ManagerCreateInputModel, Manager>();

            CreateMap<Manager, ManagerDetailsViewModel>();

            CreateMap<Manager, ManagerDeleteViewModel>();
        }
    }
}
