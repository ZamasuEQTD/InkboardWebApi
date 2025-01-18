using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Application.Medias.Abstractions.Services;
using Domain.Media.Models;
using Domain.Media.Utils;

namespace Application.Medias.Services {
    public class YoutubeEmbedService : IEmbedService
{
    public Task<Media> Create(IEmbedFileProvider url)
    {
        return Task.FromResult(new Media(
            MediaProvider.Youtube,
            YoutubeUtil.GetVideoThumbnailFromUrl(url.Url),
            YoutubeUtil.GetVideoThumbnailFromUrl(url.Url)
        ));
    }
    }
}

