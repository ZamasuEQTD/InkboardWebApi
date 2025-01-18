using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Domain.Media.Models;

namespace Application.Medias.Abstractions.Services
{
    public interface IEmbedService
    {
        Task<Media> Create(IEmbedFileProvider provider);
    }
}