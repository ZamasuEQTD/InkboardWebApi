
using Application.Core.Abstractions.Messaging;

namespace Application.Comentarios.Commands
{
    public class DenunciarComentarioCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public Guid Comentario {get; set;}
    }
}