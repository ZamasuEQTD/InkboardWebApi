using Domain.Media.Models;

namespace Domain.Media
{
    public interface IMediasRepository
    {
        Task<HashedMedia?> GetHashedMediaByHash(string hash);
    }
}