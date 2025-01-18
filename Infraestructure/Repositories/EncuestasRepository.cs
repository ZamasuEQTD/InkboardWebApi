using Domain.Encuestas;
using Domain.Encuestas.Models.ValueObjects;
using Domain.Usuarios;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class EncuestasRepository : IEncuestasRepository
    {

        private InkboardDbContext _context;

        public EncuestasRepository(InkboardDbContext context)
        {
            _context = context;
        }

        public void Add(Encuesta encuesta) => _context.Add(encuesta);
        public Task<Encuesta?> GetEncuestaById(EncuestaId id) => _context.Encuestas.FirstOrDefaultAsync(e => e.Id == id);
    }
}