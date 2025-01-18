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
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Usuario> _userManager;
        private readonly IDateTimeProvider _time;

        public DesbanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IBaneosRepository baneosRepository, IDateTimeProvider time, UserManager<Usuario> userManager)
        {
            _unitOfWork = unitOfWork;
            _baneosRepository = baneosRepository;
            _time = time;
            _userManager = userManager;
        }

        public async Task Handle(DesbanearUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _userManager.FindByIdAsync(request.Usuario.ToString());

            foreach (var baneo in await _baneosRepository.GetBaneos(usuario.Id))
            {
                baneo.Eliminar(_time.UtcNow);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}