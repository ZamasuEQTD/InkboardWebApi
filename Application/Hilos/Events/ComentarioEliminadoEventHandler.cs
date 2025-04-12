using Application.Hilos.Abstractions;
using Domain.Hilos.DomainEvents;
using MediatR;

namespace Application.Hilos.Events{
    public class ComentarioEliminadoEventHandler : INotificationHandler<ComentarioEliminadoDomainEvent>
    {
        private readonly IHiloHub _hiloHub;
        
        public ComentarioEliminadoEventHandler(IHiloHub hiloHub)
        {
            _hiloHub = hiloHub;
        }

        public async Task Handle(ComentarioEliminadoDomainEvent notification, CancellationToken cancellationToken)
        {

            Console.WriteLine($"Comentario eliminado {notification.ComentarioId}");
            
           await _hiloHub.NotificarComentarioEliminado(notification.HiloId, notification.ComentarioId);;
        }
    }
}