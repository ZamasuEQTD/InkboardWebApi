using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Domain.Comentarios;
using Domain.Core.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Models;

namespace Application.Hilos.Commands.EliminarHilo
{
    public class EliminarHiloCommandHandler : ICommandHandler<EliminarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository, IDateTimeProvider timeProvider)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
            _timeProvider = timeProvider;
        }

        public async  Task Handle(EliminarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) throw  new InvalidCommandException("Hilo no encontrado");

            hilo.Eliminar();


            await _unitOfWork.SaveChangesAsync();

        }
    }
}