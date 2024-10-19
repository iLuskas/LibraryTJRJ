using FluentValidation;
using LibraryTJRJ.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryTJRJ.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => {

            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}
