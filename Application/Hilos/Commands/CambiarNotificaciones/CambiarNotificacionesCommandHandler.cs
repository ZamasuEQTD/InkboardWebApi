using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Comentarios;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Models;

namespace Application.Hilos.Commands.CambiarNotificaciones {

public class CambiarNotificacionesHiloCommandHandler : ICommandHandler<CambiarNotificacionesHiloCommand>
{
    private readonly IHilosRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;
    public CambiarNotificacionesHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository repository, ICurrentUser user )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _user = user;
    }

    public  async Task<Result> Handle(CambiarNotificacionesHiloCommand request, CancellationToken cancellationToken)
    {
        Hilo? hilo = await  _repository.GetHiloById(new(request.HiloId));

        if(hilo is null ) return HiloErrors.NoEncontrado;

        var result = hilo.CambiarNotificaciones(new (_user.UsuarioId));

        if(result.IsFailure) return result.Error;

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
    }
}
