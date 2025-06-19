using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Application.Comentarios.Queries.GetComentarios;
using Microsoft.AspNetCore.SignalR;
using Application.Hilos.Abstractions;
namespace WebAPI.Hub
{
    public class HilosHubClients : HomeHubClients
    {
        private readonly ConcurrentDictionary<Guid, HashSet<string>> _usuariosPorHilo = new();
        private readonly ConcurrentDictionary<string, List<Guid>> _conexionesDeUsuarios = new();

        public void AgregarUsuarioAHilo(Guid hiloId, string connectionId)
        {
            _usuariosPorHilo.AddOrUpdate(hiloId,
                new HashSet<string> { connectionId },
                (_, set) => { set.Add(connectionId); return set; });

            _conexionesDeUsuarios.AddOrUpdate(connectionId,
                new List<Guid> { hiloId },
                (_, list) => { list.Add(hiloId); return list; });
        }

        public void RemoverUsuarioDeHilo(Guid hiloId, string connectionId)
        {
            if (_usuariosPorHilo.TryGetValue(hiloId, out var usuarios))
            {
                usuarios.Remove(connectionId);
            }

            if (_conexionesDeUsuarios.TryGetValue(connectionId, out var hilos))
            {
                hilos.Remove(hiloId);
            }

            // Verificar si el usuario ya no está en ningún hilo
            if (_conexionesDeUsuarios.TryGetValue(connectionId, out var hilosRestantes) && hilosRestantes.Count != 0)
            {
                base.Disconnect(connectionId);

                _conexionesDeUsuarios.TryRemove(connectionId, out _);
            }
        }

        public void DesconectarUsuarioCompletamente(string connectionId)
        {
            if (_conexionesDeUsuarios.TryRemove(connectionId, out var hilos))
            {
                foreach (var hilo in hilos)
                {
                    if (_usuariosPorHilo.TryGetValue(hilo, out var usuarios))
                    {
                        usuarios.Remove(connectionId);
                    }
                }
            }
        }


        public ReadOnlyCollection<IHubClient> ObtenerUsuariosEnHilo(Guid hiloId)
        {
            if (_usuariosPorHilo.TryGetValue(hiloId, out var usuarios))
            {
                return usuarios.Select(GetClient).ToList().AsReadOnly();
            }

            return new ReadOnlyCollection<IHubClient>([]);
        }
    }

    

    public interface IHiloHubClient
    {
        Task OnHiloComentado(GetComentarioResponse comentario);
        Task OnComentarioEliminado(string comentarioTag);
    }

    public class HiloHub : IHiloHub
    {

        private readonly IHubContext<HiloSignalrHub, IHiloHubClient> _hub;
        private readonly HilosHubClients _clients;

        public HiloHub(IHubContext<HiloSignalrHub, IHiloHubClient> hub, HilosHubClients clients)
        {
            _hub = hub;
            _clients = clients;
        }

        public async Task NotificarHiloComentado(Guid hiloId, GetComentarioResponse comentario)
        {

            var usuarios = _clients.ObtenerUsuariosEnHilo(hiloId);

            List<Task> tasks = [];

            Console.WriteLine(usuarios);

            foreach (var usuario in usuarios)
            {

                tasks.Add(_hub.Clients.Client(usuario.ConnectionId).OnHiloComentado(comentario with
                {
                    Es_Op = false,
                    Es_Autor = usuario.IsAuthenticated && usuario.UsuarioId == comentario.Autor_Id,
                    Recibir_Notificaciones = usuario.IsAuthenticated && usuario.UsuarioId == comentario.Autor_Id ? comentario.Recibir_Notificaciones : null,
                    Autor_Id = usuario.IsAuthenticated && usuario.EsModerador ? comentario.Autor_Id : null
                }));
            }

            await Task.WhenAll(tasks);
        }

        public async Task NotificarComentarioEliminado(Guid hiloId, string comentarioTag)
        {
            var usuarios = _clients.ObtenerUsuariosEnHilo(hiloId);

            List<Task> tasks = [];

            foreach (var usuario in usuarios)
            {
                tasks.Add(_hub.Clients.Client(usuario.ConnectionId).OnComentarioEliminado(comentarioTag));
            }

            await Task.WhenAll(tasks);
        }
    }

    public class HiloSignalrHub : Hub<IHiloHubClient>
    {
        private readonly HilosHubClients _clients;

        public HiloSignalrHub(HilosHubClients clients)
        {
            _clients = clients;
        }
        public override Task OnConnectedAsync()
        {
            _clients.Connect(new HubSignalrClient(Context));

            return base.OnConnectedAsync();
        }

        public void SubscribirseHilo(Guid hiloId)
        {
            var connectionId = Context.ConnectionId;

            _clients.AgregarUsuarioAHilo(hiloId, connectionId);
        }

        public void DesubscribirseHilo(Guid hiloId)
        {
            var connectionId = Context.ConnectionId;

            _clients.RemoverUsuarioDeHilo(hiloId, connectionId);
        }


        override public Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            _clients.DesconectarUsuarioCompletamente(connectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}