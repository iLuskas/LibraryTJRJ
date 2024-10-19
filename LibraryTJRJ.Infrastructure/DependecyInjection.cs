using Asp.Versioning;
using LibraryTJRJ.Application.Common.Interfaces.Authentication;
using LibraryTJRJ.Application.Common.Interfaces.Services;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Interfaces;
using LibraryTJRJ.Domain.Subjects;
using LibraryTJRJ.Domain.User;
using LibraryTJRJ.Infrastructure.Authentication;
using LibraryTJRJ.Infrastructure.Authors.Persistence;
using LibraryTJRJ.Infrastructure.Books.Persistence;
using LibraryTJRJ.Infrastructure.Common.Persistence;
using LibraryTJRJ.Infrastructure.Services;
using LibraryTJRJ.Infrastructure.Subjects;
using LibraryTJRJ.Infrastructure.Users.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryTJRJ.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        AddPersistence(services);
        
        AddAuth(services, configuration);

        return services;
    }

    public static IServiceCollection AddPersistence(IServiceCollection services)
    {
        services.AddDbContext<LibraryTJRJDbContext>(options =>
            options.UseSqlite("Data Source = app_data/LibraryTJRJ.db"));

        //services.AddDbContext<LibraryTJRJDbContext>(options =>
        //    options.UseSqlite("Data Source = LibraryTJRJ.db"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<LibraryTJRJDbContext>());

        return services;
    }

    public static IServiceCollection AddAuth(IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();

        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(authOptions => {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", opt => {
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });

        return services;
    }
}
