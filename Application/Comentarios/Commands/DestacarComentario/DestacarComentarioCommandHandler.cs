using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Comentarios.Models;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Models;

namespace Application.Comentarios.Commands.DestacarComentario
{
    public class DestacarComentarioCommandHandler : ICommandHandler<DestacarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly ICurrentUser _user;
        private readonly IUnitOfWork _unitOfWork;

        public DestacarComentarioCommandHandler(IUnitOfWork unitOfWork, ICurrentUser user, IComentariosRepository comentariosRepository, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _comentariosRepository = comentariosRepository;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(DestacarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new(request.Comentario));

            if (hilo is null || comentario is null) return HiloErrors.NoEncontrado;

            Result result = hilo.DestacarComentario(
                new(_user.UsuarioId),
                comentario
            );
            
            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}