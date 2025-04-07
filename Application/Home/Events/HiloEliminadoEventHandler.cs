using Application.Home.Abstractions;
using Domain.Hilos.DomainEvents;
using MediatR;

namespace Application.Home.Events
{
    public class HiloEliminadoEventHandler : INotificationHandler<HiloEliminadoDomainEvent>
    {
        private readonly IHomeHub _hub;

        public HiloEliminadoEventHandler(IHomeHub hub)
        {
            _hub = hub;
        }

        public Task Handle(HiloEliminadoDomainEvent notification, CancellationToken cancellationToken)
        {
            return _hub.NotificarHiloEliminado(notification.HiloId);
        }
    }
}