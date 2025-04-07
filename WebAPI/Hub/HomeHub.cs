using System.Security.Claims;
using Application.Core.Abstractions;
using Application.Hilos.Queries.GetPortadas;
using Application.Home.Abstractions;
using Domain.Core;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hub
{
    public interface IHubClient : ICurrentUser
    {
        string ConnectionId {get;}
    }
   
    public class HomeHub : IHomeHub
    {

        private readonly IHubContext<HomeSignalrHub, IHomeHubClient> _hub;
        private readonly HomeHubClients _clients;

        public HomeHub(IHubContext<HomeSignalrHub, IHomeHubClient> hub, HomeHubClients clients)
        {
            _hub = hub;
            _clients = clients;
        }

        public Task NotificarHiloEliminado(Guid hiloId) => _hub.Clients.All.OnHiloEliminado(hiloId);

        public async Task NotificarHiloPosteado(GetPortadaResponse portada)
        {

            List<Task> tasks = [];

            foreach (var client in _clients.Clientes)
            {

                GetPortadaResponse response = portada with
                {
                    Autor_Id = client.IsAuthenticated && portada.Autor_Id == client.UsuarioId ? portada.Autor_Id : null,
                    Recibir_Notificaciones = client.IsAuthenticated && portada.Autor_Id == client.UsuarioId ? portada.Recibir_Notificaciones : null,
                    Es_Op = client.IsAuthenticated && portada.Autor_Id == client.UsuarioId && portada.Es_Op,  
                };

                tasks.Add(_hub.Clients.Client(client.ConnectionId).OnHiloPosteado(response));
            }

            await Task.WhenAll(tasks);
        }
    }

    public class HomeHubClients
    {
        private Dictionary<string, IHubClient> _clientes = new();

        public void Connect(IHubClient client) => _clientes.Add(client.ConnectionId, client);

        public void Disconnect(string connectionId) => _clientes.Remove(connectionId);
    
        public IHubClient GetClient(string connectionId) => _clientes[connectionId];

        public List<IHubClient> Clientes => _clientes.Values.ToList();

    }

    public class HomeSignalrHub: Hub<IHomeHubClient>
    {

        private readonly HomeHubClients clients;

        public HomeSignalrHub(HomeHubClients clients)
        {
            this.clients = clients;
        }

        public override Task OnConnectedAsync()
        {
            var user = new HubSignalrClient(Context);

            clients.Connect(user);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {

            clients.Disconnect(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }

    public interface IHomeHubClient {
        Task OnHiloEliminado(Guid hiloId);

        Task OnHiloPosteado(GetPortadaResponse portada);
    }

    public class HubSignalrClient : IHubClient
    {

        private readonly HubCallerContext _context;

        public HubSignalrClient(HubCallerContext context)
        {
            _context = context;
        }

        public string ConnectionId =>_context.ConnectionId;

        public bool IsAuthenticated => _context.User?
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("No hay current user disponible");

        public Guid UsuarioId => Guid.Parse(_context.User!.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)!.Value);

        public string Username => throw new NotImplementedException();

        public List<string> Roles => _context.User.Claims.Where(s => s.Type == ClaimTypes.Role).Select(c=> c.Value).ToList() ;

        public bool EsModerador => Roles.Contains(AppRoles.Moderador);
    }
}