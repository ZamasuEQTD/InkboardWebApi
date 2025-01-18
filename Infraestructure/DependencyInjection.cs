
using Application.Core.Abstractions;
using Infraestructure.Authentication;
using Infraestructure.Authentication.Jwt;
using Infraestructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class DependencyInjection
    {        
        static public IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            return services;
        }

        
    }
}