
using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.SeguirHilo
{
    public class SeguirHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
    }
}