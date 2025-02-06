using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Core;
using Domain.Notificaciones;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Notificaciones.Commands.LeerNotificaciones
{
    public class LeerNotificacionesCommandHandler : ICommandHandler<LeerNotificacionesCommand>
    {
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly ICurrentUser _user;
        private readonly IUnitOfWork _unitOfWork;

        public LeerNotificacionesCommandHandler(IUnitOfWork unitOfWork, ICurrentUser user, INotificacionesRepository notificacionesRepository)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _notificacionesRepository = notificacionesRepository;
        }

        public async Task<Result> Handle(LeerNotificacionesCommand request, CancellationToken cancellationToken)
        {
            List<Notificacion> notificaciones = await _notificacionesRepository.GetNotificacionesDeUsuarioById(new IdentityId(_user.UsuarioId));

            foreach (var n in notificaciones)
            {
                n.Leer(
                    new(_user.UsuarioId)
                );
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}