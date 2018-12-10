namespace TeamLegend.Infrastructure.Extensions
{
    using CustomMiddlewares;

    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedRolesMiddleware>();
        }
    }
}
