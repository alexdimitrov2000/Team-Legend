namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Common;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AdministrationController : Controller
    {
    }
}
