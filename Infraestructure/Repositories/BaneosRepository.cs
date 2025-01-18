using Domain.Baneos;
using Domain.Baneos.Models.ValueObjects;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class BaneosRepository : IBaneosRepository
    {
        public InkboardDbContext _context;
        public BaneosRepository(InkboardDbContext context)
        {
            _context = context;
        }

        public void Add(Baneo baneo) => _context.Add(baneo);

        public Task<Baneo> GetBaneoById(BaneoId id) => _context.Baneos.FirstAsync(b => b.Id == id);
        public Task<List<Baneo>> GetBaneos(IdentityId usuarioId) => _context.Baneos.Where(b => b.UsuarioBaneadoId == usuarioId).ToListAsync();
    }
}