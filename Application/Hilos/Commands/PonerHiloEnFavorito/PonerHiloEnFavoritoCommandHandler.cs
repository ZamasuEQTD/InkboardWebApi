using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Hilos.Commands.PonerHiloEnFavorito;

public class PonerHiloEnFavoritoCommandHandler : ICommandHandler<PonerHiloEnFavoritoCommand>
{
    private readonly IHilosRepository _hilosRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;
    public PonerHiloEnFavoritoCommandHandler(IHilosRepository hiloRepository, IUnitOfWork unitOfWork, ICurrentUser user)
    {
        _hilosRepository = hiloRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }
    public async Task Handle(PonerHiloEnFavoritoCommand request, CancellationToken cancellationToken)
    {
        Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

        if (hilo is null) throw new InvalidCommandException("Hilo no encontrado");

        hilo.RealizarInteraccion(HiloInteraccion.Acciones.Favorito,new IdentityId(_user.UsuarioId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }

}