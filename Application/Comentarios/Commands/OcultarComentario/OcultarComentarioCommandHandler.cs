using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Comentarios;
using Domain.Comentarios.Models;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Comentarios.Commands.OcultarComentario
{
    public class OcultarComentarioCommandHandler : ICommandHandler<OcultarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly ICurrentUser _context;
        private readonly IUnitOfWork _unitOfWork;

        public OcultarComentarioCommandHandler(IUnitOfWork unitOfWork, ICurrentUser context, IComentariosRepository comentariosRepository, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _comentariosRepository = comentariosRepository;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(OcultarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null)  return HiloErrors.NoEncontrado;

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new(request.Comentario));

            if (comentario is null) return ComentarioErrors.NoEncontrado;

            comentario.Ocultar(hilo, new IdentityId(_context.UsuarioId));

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}