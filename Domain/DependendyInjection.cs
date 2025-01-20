using Domain.Comentarios.Abstractions.Services;
using Domain.Comentarios.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain {
        public static class DependencyInjection {
        static public IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IColorService, ColorService>();

            return services;
        }
    }
}