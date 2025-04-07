using System.Threading.Tasks;
using Application.Core.Abstractions;
using Application.Hilos.Queries.GetPortadas;
using Application.Home.Abstractions;
using Dapper;
using Domain.Hilos.DomainEvents;
using MediatR;

namespace Application.Home.Events
{
    public class HiloPosteadoEventHandler : INotificationHandler<HiloPosteadoDomainEvent>
    {
        private readonly IHomeHub _hub;
        private readonly IDBConnectionFactory _connection;

        public HiloPosteadoEventHandler(IHomeHub hub, IDBConnectionFactory connection)
        {
            _hub = hub;
            _connection = connection;
        }

        public async Task Handle(HiloPosteadoDomainEvent notification, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            var hilo = await connection.QueryAsync<GetPortadaResponse, GetBanderas, GetPortadaMiniatura, GetPortadaResponse>(@"
                SELECT * FROM HiloPortadaView                     
                WHERE
                    id = @Id
            ",
            (portada, banderas, imagen) =>
            {
                portada.Miniatura = imagen;
                portada.Banderas = banderas;
                return portada;
            },
            new
            {
                Id = notification.HiloId
            },
            splitOn: "es_sticky, spoiler");

            if (hilo is not null && hilo.Any())
            {
                var portada = hilo.First();
                
                await _hub.NotificarHiloPosteado(portada);
            }
        }
    }
}
