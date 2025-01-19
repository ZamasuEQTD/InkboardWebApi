using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Hilos.Commands.DenunciarHilo
{
    public class DenunciarHiloCommandHandler : ICommandHandler<DenunciarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly ICurrentUser _user;
        private readonly IUnitOfWork _unitOfWork;

        public DenunciarHiloCommandHandler(IUnitOfWork unitOfWork, ICurrentUser user, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _hilosRepository = hilosRepository;
        }

        public async Task Handle(DenunciarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) throw new DomainBusinessException("Hilo no encontrado");

            hilo.Denunciar(new IdentityId(_user.UsuarioId));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}