using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Core;
using Domain.Notificaciones;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Notificaciones.Commands.LeerNotificacion
{
    public class LeerNotificacionCommandHandler : ICommandHandler<LeerNotificacionCommand>
    {

        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly ICurrentUser _context;
        private readonly IUnitOfWork _unitOfWork;

        public LeerNotificacionCommandHandler(IUnitOfWork unitOfWork, ICurrentUser context, INotificacionesRepository notificacionesRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _notificacionesRepository = notificacionesRepository;
        }

        public async Task<Result> Handle(LeerNotificacionCommand request, CancellationToken cancellationToken)
        {
            Notificacion? notificacion = await _notificacionesRepository.GetNotificacionById(new(request.Id));

            IdentityId usuarioId = new IdentityId(_context.UsuarioId);

            if (!notificacion!.EsUsuarioNotificado(usuarioId)) return NotificacionesFailures.NoTePertenece;

            Result result = notificacion.Leer(usuarioId);

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync(); 

            return Result.Success();
        }
    }
}