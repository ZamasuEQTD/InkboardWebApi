
using Application.Core.Abstractions.Messaging;

namespace Application.Comentarios.Commands.OcultarComentario
{
    public class OcultarComentarioCommand : ICommand {
        public Guid Comentario { get; set; }
        public Guid Hilo { get; set; }
    }
}