using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Application.Medias.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Medias.Services
{
    public class FileServiceFactoryScoped : IFileServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public FileServiceFactoryScoped(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileService Create(FileType tipo)
        {
            return tipo switch
            {
                FileType.Imagen => _serviceProvider.GetRequiredService<ImagenService>(),
                FileType.Video => _serviceProvider.GetRequiredService<VideoService>(),
                FileType.Gif => _serviceProvider.GetRequiredService<GifService>(),
                _ => throw new ArgumentException($"Tipo de media inválido: {tipo}"),
            };
        }
    }
}