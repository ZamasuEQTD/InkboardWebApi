using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class HilosRepository : IHilosRepository
    {
        private readonly InkboardDbContext _context;

        public HilosRepository(InkboardDbContext context)
        {
            _context = context;
        }

        public void Add(Hilo hilo) => _context.Add(hilo);
        public Task<Hilo?> GetHiloById(HiloId id) => _context.Hilos.Include(h=> h.Comentarios).FirstOrDefaultAsync(h => h.Id == id);
    }
}