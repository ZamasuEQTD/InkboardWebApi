using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;

namespace Application.Hilos.Commands.EliminarSticky
{

    public class EliminarStickyCommandHandler : ICommandHandler<EliminarStickyCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarStickyCommandHandler(IUnitOfWork unitOfWork, IDateTimeProvider timeProvider, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
            _hilosRepository = hilosRepository;
        }

        public async Task Handle(EliminarStickyCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) throw new DomainBusinessException("Hilo no encontrado");

            hilo.EliminarSticky();


            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }

}