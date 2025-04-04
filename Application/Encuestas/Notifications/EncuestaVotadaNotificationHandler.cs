using Application.Encuestas.Abstractions;
using Domain.Encuestas.DomainEvents;
using MediatR;

namespace Application.Encuestas.Notifications {
    public class EncuestaVotadaNotificationHandler : INotificationHandler<EncuestaVotadaDomainEvent>
    {
        public IEncuestaHub hub;

        public EncuestaVotadaNotificationHandler(IEncuestaHub hub)
        {
            this.hub = hub;
        }

        public Task Handle(EncuestaVotadaDomainEvent notification, CancellationToken cancellationToken) => hub.EnviarEncuestaVotada(notification.EncuestaId, notification.RespuestaId);
    }
}