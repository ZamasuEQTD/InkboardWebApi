using Application.Notificaciones.Abstractions;
using Application.Notificaciones.Queries.GetNotificacionesQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hub
{
    public class NotificacionesHub : INotificacionesHub
    {
        private readonly IHubContext<NotificacionesSignalRHub, INotificacionesHubClient> _hub;

        public NotificacionesHub(IHubContext<NotificacionesSignalRHub, INotificacionesHubClient> hub)
        {
            _hub = hub;
        }

        public Task EnviarNotificacion(GetNotificacionResponse notificacion, Guid usuario)
        {
            return _hub.Clients.Group(usuario.ToString()).OnNotificacionRecibida(notificacion);
        }
    }

    public interface INotificacionesHubClient
    {
        Task OnNotificacionRecibida(GetNotificacionResponse notificacion);
    }



    [Authorize]
    public class NotificacionesSignalRHub : Hub<INotificacionesHubClient>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
           
            HubSignalrClient client = new(Context);

            await Groups.AddToGroupAsync(Context.ConnectionId, client.UsuarioId.ToString());
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

       
    }
}