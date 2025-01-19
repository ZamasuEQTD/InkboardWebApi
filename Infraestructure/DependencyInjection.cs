
using Application.Core.Abstractions;
using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Application.Medias.Services;
using Domain.Baneos;
using Domain.Categorias;
using Domain.Comentarios;
using Domain.Encuestas;
using Domain.Hilos;
using Domain.Media;
using Domain.Usuarios;
using Infraestructure.Authentication;
using Infraestructure.Authentication.Jwt;
using Infraestructure.Media;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Infraestructure.Services.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class DependencyInjection
    {        
        static public IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            
            services.AddScoped<IDBConnectionFactory, NpgsqlConnectionFactory>();

            services.AddScoped<IVideoGifPrevisualizadorService, FfmpegVideoVistaPreviaService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IImageResizer, Resizer>();
            services.AddScoped<IMediaUrlProvider, UrlConfigurationProvider>();

            services.AddScoped<IFileStorageService, FileStorageService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddScoped<IHilosRepository, HilosRepository>();
            services.AddScoped<IBaneosRepository, BaneosRepository>();
            services.AddScoped<ICategoriasRepository, CategoriasRepository>();
            services.AddScoped<IComentariosRepository, ComentariosRepository>();
            services.AddScoped<IEncuestasRepository, EncuestasRepository>();
            services.AddScoped<IMediasRepository, MediasRepository>();

            return services;
        }

        
    }
}