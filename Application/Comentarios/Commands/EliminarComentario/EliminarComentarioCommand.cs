
using Application.Core.Abstractions.Messaging;

namespace Application.Comentarios.Commands.EliminarComentari
{
    public class EliminarComentarioCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public Guid Comentario { get; set; }
    }
}