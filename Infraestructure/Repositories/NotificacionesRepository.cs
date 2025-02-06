using Domain.Notificaciones;
using Domain.Notificaciones.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class NotificacionesRepository : INotificacionesRepository
    {
        private readonly InkboardDbContext _context;

        public NotificacionesRepository(InkboardDbContext context)
        {
            _context = context;
        }

        public Task<Notificacion?> GetNotificacionById(NotificacionId id) => this._context.Notificaciones.FirstOrDefaultAsync(n => n.Id == id);

        public Task<List<Notificacion>> GetNotificacionesDeUsuarioById(IdentityId id)  => this._context.Notificaciones.Where(n=> n.NotificadoId == id).ToListAsync();
    }
}