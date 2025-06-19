using Application.Encuestas.Abstractions;
using Application.Hilos.Abstractions;
using Application.Home.Abstractions;
using Application.Medias.Abstractions.Providers;
using Application.Notificaciones.Abstractions;
using Infraestructure.Services.Providers;
using WebAPI.Hub;

namespace WebAPI
{
    public static class DependencyInjection
    {        
        static public IServiceCollection AddWebApi(this IServiceCollection services)
        { 
            services.AddScoped<IMediaFolderProvider, FolderProvider>();

            services.AddScoped<IEncuestaHub, EncuestasHub>();

            services.AddScoped<IHomeHub, HomeHub>();

            services.AddSingleton<HomeHubClients>();

            services.AddSingleton<HilosHubClients>();

            services.AddScoped<IHiloHub, HiloHub>();

            services.AddSingleton<INotificacionesHub, NotificacionesHub>();
            return services;
        }
    }
}