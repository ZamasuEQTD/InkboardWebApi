using Domain.Media.Models;

namespace Domain.Media
{
    public interface IMediasRepository
    {
        void Add(HashedMedia media);
        void Add(MediaSpoileable reference);
        Task<HashedMedia?> GetHashedMediaByHash(string hash);
    }
}