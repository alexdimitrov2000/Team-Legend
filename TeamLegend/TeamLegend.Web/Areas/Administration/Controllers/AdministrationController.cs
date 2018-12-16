namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
    }
}
