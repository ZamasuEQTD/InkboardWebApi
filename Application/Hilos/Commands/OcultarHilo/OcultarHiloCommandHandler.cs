using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;
using Domain.Core;

namespace Application.Hilos.Commands.OcultarHilo {
    
public class OcultarHiloCommandHandler : ICommandHandler<OcultarHiloCommand>
{
    private readonly IHilosRepository _hilosRepository;   
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;
    public OcultarHiloCommandHandler(IHilosRepository hiloRepository, IUnitOfWork unitOfWork, ICurrentUser user)
    {
        _hilosRepository = hiloRepository;
        _unitOfWork = unitOfWork;
        _user = user;   
    }
    public async Task<Result> Handle(OcultarHiloCommand request, CancellationToken cancellationToken)
    {
        Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

        if (hilo is null) return HiloErrors.NoEncontrado;

        var result = hilo.RealizarInteraccion(HiloInteraccion.Acciones.Ocultar, new IdentityId(_user.UsuarioId));
        
        if (result.IsFailure) return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
}