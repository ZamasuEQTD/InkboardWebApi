
using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.OcultarHilo {
    public class OcultarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
    }
}