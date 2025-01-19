
using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.EliminarSticky
{
    public class EliminarStickyCommand : ICommand
    {
        public Guid Hilo { get; set; }

    }
}