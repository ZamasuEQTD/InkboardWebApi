using Application.Core.Abstractions.Messaging;

namespace Application.Comentarios.Commands.DestacarComentario
{
    public class DestacarComentarioCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public Guid Comentario { get; set; }

        public DestacarComentarioCommand(Guid hilo, Guid comentario)
        {
            Hilo = hilo;
            Comentario = comentario;
        }

    }
}