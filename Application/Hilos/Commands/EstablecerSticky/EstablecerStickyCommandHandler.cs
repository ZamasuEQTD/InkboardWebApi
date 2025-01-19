using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Comentarios;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Models;

namespace Application.Hilos.Commands.EstablecerSticky
{
    public class EstablecerStickyCommandHandler : ICommandHandler<EstablecerStickyCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EstablecerStickyCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(EstablecerStickyCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) return  HiloErrors.NoEncontrado;

            var result = hilo.EstablecerSticky();
            
            if (result.IsFailure) return result;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}