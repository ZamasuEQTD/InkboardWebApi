using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Comentarios.Models;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;

namespace Application.Comentarios.Commands.DenunciarComentario{
    public class DenunciarComentarioCommandHandler : ICommandHandler<DenunciarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly ICurrentUser _context;
        private readonly IUnitOfWork _unitOfWork;

        public DenunciarComentarioCommandHandler(IUnitOfWork unitOfWork, ICurrentUser context, IHilosRepository hilosRepository, IComentariosRepository comentariosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _hilosRepository = hilosRepository;
            _comentariosRepository = comentariosRepository;
        }

        public async Task<Result> Handle(DenunciarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new(request.Comentario));

            if (hilo is null) return HiloErrors.NoEncontrado;

            if (comentario is null) return ComentarioErrors.NoEncontrado;

            var result = comentario.Denunciar(
                hilo,
                new(_context.UsuarioId)
            );

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}