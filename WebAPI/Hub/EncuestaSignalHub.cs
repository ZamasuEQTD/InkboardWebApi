using Microsoft.AspNetCore.SignalR;
using Application.Encuestas.Abstractions;
namespace WebAPI.Hub
{
    public class EncuestasHub : IEncuestaHub
    {
        private readonly IHubContext<EncuestaSignalrHub, IEncuestaClientSignalrHub> _hubContext;

        public EncuestasHub(IHubContext<EncuestaSignalrHub, IEncuestaClientSignalrHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task EnviarEncuestaVotada(Guid encuestaId, Guid respuestaId) => await _hubContext.Clients.Group(encuestaId.ToString()).OnEncuestaVotada(respuestaId);
    }
    public interface IEncuestaClientSignalrHub
    {
        Task OnEncuestaVotada(Guid respuesta);
    }
    public class EncuestaSignalrHub : Hub<IEncuestaClientSignalrHub> {
        public async Task Unirse(Guid encuestaId) => await  Groups.AddToGroupAsync(Context.ConnectionId, encuestaId.ToString());
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}

