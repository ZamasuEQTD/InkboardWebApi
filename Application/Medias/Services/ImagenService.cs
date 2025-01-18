
using Application.Medias.Abstractions.Providers;
using Application.Medias.Abstractions.Services;
using Domain.Media.Models;

namespace Application.Medias.Services {
    
    public class ImagenService : IFileService
    {
        private readonly MiniaturaService _miniaturaService;
        private readonly IMediaUrlProvider _url;
        public ImagenService(MiniaturaService miniaturaService, IMediaUrlProvider url)
        {
            _miniaturaService = miniaturaService;
            _url = url;
        }

        public async Task<Media> Create(string path)
        {
            string miniatura = await _miniaturaService.Procesar(path);

            return new Media(
                MediaProvider.Imagen,
                _url.Thumbnail + Path.GetFileName(miniatura),
                null
            );
        }
    }
}
