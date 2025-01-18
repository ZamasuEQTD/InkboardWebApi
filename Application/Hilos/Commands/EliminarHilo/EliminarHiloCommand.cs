

using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.EliminarHilo
{
    public class EliminarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
    }
}