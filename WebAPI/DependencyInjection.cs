using Application.Medias.Abstractions.Providers;
using Infraestructure.Services.Providers;

namespace WebAPI
{
    public static class DependencyInjection
    {        
        static public IServiceCollection AddWebApi(this IServiceCollection services)
        { 
            services.AddScoped<IMediaFolderProvider, FolderProvider>();

            return services;
        }
    }
}