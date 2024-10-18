using LibraryTJRJ.Api.Common.ErrorsBehavior;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibraryTJRJ.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, LibraryTJRJProblemDetailsFactory>();

        return services;
    }
}
