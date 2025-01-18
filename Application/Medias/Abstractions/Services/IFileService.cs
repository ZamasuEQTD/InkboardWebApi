using Domain.Media.Models;

namespace Application.Medias.Abstractions.Services
{
    public interface IFileService
    {
        Task<Media> Create(string path);
    }
}