
using Application.Core.Abstractions;
using Infraestructure.Authentication.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class DependencyInjection
    {        
        static public IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            
            return services;
        }

        
    }
}