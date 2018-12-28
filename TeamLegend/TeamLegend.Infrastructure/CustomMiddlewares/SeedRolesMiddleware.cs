namespace TeamLegend.Infrastructure.CustomMiddlewares
{
    using Common;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    using System.Linq;
    using System.Threading.Tasks;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        
        public async Task InvokeAsync(HttpContext httpContext, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdminRole));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            await this.next(httpContext);

        }
    }
}
