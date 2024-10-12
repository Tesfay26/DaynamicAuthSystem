using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Infrastructure;
using DynamicAuthSystem.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using System.Reflection;
using DynamicAuthSystem.Domain.Interfaces;
using DynamicAuthSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using DynamicAuthSystem.Application.CommandHandler;

namespace DynamicAuthSystem.API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures the database connection using Entity Framework and SQL Server.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        /// <param name="configuration">IConfiguration instance containing the connection string.</param>
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// Configures ASP.NET Core Identity, linking it with Entity Framework stores for persistence.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Configures JWT-based authentication, validating tokens based on configuration.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        /// <param name="configuration">IConfiguration instance containing JWT settings.</param>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]))
                };
            });
        }

        /// <summary>
        /// Registers application-specific services such as IUserService and IRolePermissionService.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
           // services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Registers MediatR handlers for handling commands and queries in the application.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        public static void RegisterMediatRHandlers(this IServiceCollection services)
        {
            // Registers MediatR and finds all handlers in the current assembly.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRolePermissionHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginUserCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LogoutUserCommandHandler).Assembly));    
        }

        /// <summary>
        /// Registers all validators found in the current assembly using FluentValidation.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        public static void RegisterValidators(this IServiceCollection services)
        {
            // Registers FluentValidation validators.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Registers AutoMapper profiles, enabling automatic object-object mapping.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            // Registers AutoMapper and finds all profiles in the current assembly.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
