using Asp.Versioning;
using LibraryTJRJ.Api.Common.ErrorsBehavior;
using LibraryTJRJ.Api.OpenApi;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace LibraryTJRJ.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        AddApiVersioning(services);

        services.AddSwaggerGen();

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        services.AddSingleton<ProblemDetailsFactory, LibraryTJRJProblemDetailsFactory>();

        return services;
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
