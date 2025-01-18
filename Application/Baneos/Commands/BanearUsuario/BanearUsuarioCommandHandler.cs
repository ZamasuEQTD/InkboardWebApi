using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Baneos;
using Domain.Baneos.Models.Enums;
using Domain.Core.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Baneos.Commands.BanearUsuario
{
    public class BanearUsuarioCommandHandler : ICommandHandler<BanearUsuarioCommand>
    {
        private readonly IBaneosRepository _baneosRepository;
        private readonly ICurrentUser _user;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Usuario> _userManager;
        public BanearUsuarioCommandHandler(IUnitOfWork unitOfWork, ICurrentUser context,   IBaneosRepository baneosRepository, IDateTimeProvider timeProvider, UserManager<Usuario> userManager)
        {
            _unitOfWork = unitOfWork;
            _user = context;
            _baneosRepository = baneosRepository;
            _userManager = userManager;
        }

        public async Task  Handle(BanearUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _userManager.FindByIdAsync(request.UsuarioId.ToString());

            if (usuario is null || (await _userManager.GetRolesAsync(usuario)).Any(r => r != "Anonimo")) throw new InvalidCommandException("Solamente puedes banear a usuarios anonimos");

            DateTime? finalizacion = null;

            if(request.Duracion is not null) finalizacion = request.Duracion.Value.ToDuracion();

            Baneo baneo = new(
                new(_user.UsuarioId),
                usuario.Id,
                finalizacion,
                request.Mensaje,
                request.Razon
            );

            _baneosRepository.Add(baneo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    static class BaneoExtensions {
        public static DateTime ToDuracion(this DuracionBaneo duracion) {
            DateTime time = DateTime.UtcNow;
            return duracion switch {
                DuracionBaneo.CincoMinutos => time.AddMinutes(5),
                DuracionBaneo.UnaHora => time.AddHours(1),
                DuracionBaneo.UnDia => time.AddDays(1),
                DuracionBaneo.UnaSemana => time.AddDays(7),
                DuracionBaneo.UnMes => time.AddMonths(1),
                DuracionBaneo.Permanente => time.AddYears(1000),
                _ => throw new ArgumentException("Duracion de baneo no soportada")
            };
        }
    }
}