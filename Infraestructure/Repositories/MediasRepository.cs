using Application.Hilos.Commands;
using Domain.Media;
using Domain.Media.Models;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MediasRepository : IMediasRepository
    {
        private readonly InkboardDbContext _context;

        public MediasRepository(InkboardDbContext context)
        {
            _context = context;
        }

        public void Add(HashedMedia media) => _context.Add(media);
        public void Add(MediaSpoileable reference) => _context.Add(reference);
        public Task<HashedMedia?> GetHashedMediaByHash(string hash) => _context.Medias.FirstOrDefaultAsync(m => m.Hash == hash);
    }
}