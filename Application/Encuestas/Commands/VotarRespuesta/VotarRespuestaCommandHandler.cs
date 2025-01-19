using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Application.Encuestas.Commands.VotarRespuesta;
using Domain.Encuestas;
using Domain.Encuestas.Models.ValueObjects;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Encuestas.Commands.VotarRespuesta {
    public class VotarRespuestaCommandHandler : ICommandHandler<VotarRespuestaCommand> {
    private readonly IEncuestasRepository _encuestasRepository;
    private readonly ICurrentUser _user;
    private readonly IUnitOfWork _unitOfWork;

    public VotarRespuestaCommandHandler(IEncuestasRepository encuestasRepository, ICurrentUser user, IUnitOfWork unitOfWork)
    {
        _encuestasRepository = encuestasRepository;
        _user = user;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(VotarRespuestaCommand request, CancellationToken cancellationToken)
    {
        var encuesta = await _encuestasRepository.GetEncuestaById(new EncuestaId(request.EncuestaId));

        if (encuesta is null)  throw new InvalidCommandException("Encuesta no encontrada");

        encuesta.Votar(new IdentityId(_user.UsuarioId), new RespuestaId(request.RespuestaId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    }
}   

