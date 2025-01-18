using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Baneos;
using Domain.Core.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Baneos.Commands.DesbanearUsuario
{
    public class DesbanearUsuarioCommandHandler : ICommandHandler<DesbanearUsuarioCommand>
    {
        private readonly IBaneosRepository _baneosRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Usuario> _userManager;
        private readonly IDateTimeProvider _time;

        public DesbanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IUsuariosRepository usuariosRepository, IBaneosRepository baneosRepository, IDateTimeProvider time, UserManager<Usuario> userManager)
        {
            _unitOfWork = unitOfWork;
            _usuariosRepository = usuariosRepository;
            _baneosRepository = baneosRepository;
            _time = time;
            _userManager = userManager;
        }

        public async Task Handle(DesbanearUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuariosRepository.GetUsuarioById(new(request.Usuario));

            foreach (var baneo in await _baneosRepository.GetBaneos(usuario.Id))
            {
                baneo.Eliminar(_time.UtcNow);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}