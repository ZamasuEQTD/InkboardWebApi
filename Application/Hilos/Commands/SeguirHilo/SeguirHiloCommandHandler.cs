using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;
using Domain.Core;

namespace Application.Hilos.Commands.SeguirHilo
{
    public class SeguirHiloCommandHandler : ICommandHandler<SeguirHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly ICurrentUser _user;
        private readonly IUnitOfWork _unitOfWork;

        public SeguirHiloCommandHandler(IUnitOfWork unitOfWork, ICurrentUser user, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(SeguirHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) return HiloErrors.NoEncontrado;

            var result = hilo.RealizarInteraccion(HiloInteraccion.Acciones.Seguir, new IdentityId(_user.UsuarioId));
           
            if (result.IsFailure) return result;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}