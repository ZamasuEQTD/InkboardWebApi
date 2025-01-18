using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.Medias.Abstractions;
using Application.Medias.Services;
using Application.Core.Abstractions;
using Application.Core.Services;
using Domain.Core.Abstractions;
namespace Application
{
    public static class DependencyInjection
    {
        static public IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.Configuration.AssemblyReference.Assembly))
            .Scan(x => x.FromAssemblies(Configuration.AssemblyReference.Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces().WithScopedLifetime());

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IHasher, Hasher>();

            services.AddScoped<IFileServiceFactory, FileServiceFactoryScoped>();
            services.AddScoped<IEmbedServiceFactory, EmbedMediaFactoryServiceScoped>();
            services.AddScoped<YoutubeEmbedService>();
            services.AddScoped<EmbedProcesador>();

            services.AddScoped<MiniaturaService>();

            services.AddScoped<GifVideoPrevisualizadorProcesador>();

            services.AddScoped<VideoService>();
            services.AddScoped<ImagenService>();
            services.AddScoped<GifService>();
            services.AddScoped<MediaProcesador>();

            return services;
        }
    }
}