using Domain.Comentarios;
using Domain.Comentarios.Models;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Denuncias;
using Domain.Hilos;
using Domain.Usuarios;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ComentariosRepository : IComentariosRepository
    {
        private readonly InkboardDbContext _context;
        public ComentariosRepository(InkboardDbContext context)
        {
            _context = context;
        }

        public Task<Comentario?> GetComentarioById(ComentarioId id) => _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

    }
}