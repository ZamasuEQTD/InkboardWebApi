using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Hilos;
using Domain.Comentarios;
using Domain.Comentarios.Models;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios;

namespace Application.Comentarios.Commands.EliminarComentari
{
    public class EliminarComentarioCommandHandler : ICommandHandler<EliminarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarComentarioCommandHandler(IUnitOfWork unitOfWork, IComentariosRepository comentariosRepository, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _comentariosRepository = comentariosRepository;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(EliminarComentarioCommand request, CancellationToken cancellationToken)
        {

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new ComentarioId(request.Comentario));

            if (comentario is null) return ComentarioErrors.NoEncontrado;

            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) return HiloErrors.NoEncontrado;

            if (hilo.Id != comentario.HiloId) return ComentarioErrors.NoEncontrado;

            var result = comentario.Eliminar(hilo);

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}