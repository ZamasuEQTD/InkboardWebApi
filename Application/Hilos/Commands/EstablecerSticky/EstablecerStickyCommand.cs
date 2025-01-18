
using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.EstablecerSticky
{
    public class EstablecerStickyCommand : ICommand
    {
        public Guid Hilo { get; set; }
    }
}