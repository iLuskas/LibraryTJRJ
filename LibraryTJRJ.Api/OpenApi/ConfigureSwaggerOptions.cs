using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryTJRJ.Api.OpenApi;


internal sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Insira o token JWT no campo abaixo. \r\n\r\nExemplo: 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
       Configure(options);

    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
    {
        var openApiInfo = new OpenApiInfo
        {
            Title = $"LibraryTJRJ.Api v{apiVersionDescription.ApiVersion}",
            Version = apiVersionDescription.ApiVersion.ToString()
        };

        if (apiVersionDescription.IsDeprecated)
        {
            openApiInfo.Description += " This API version has been deprecated.";
        }

        return openApiInfo;
    }
}
