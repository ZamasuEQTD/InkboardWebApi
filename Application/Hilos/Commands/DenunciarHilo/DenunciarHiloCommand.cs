
using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.DenunciarHilo
{
    public class DenunciarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
    }
}