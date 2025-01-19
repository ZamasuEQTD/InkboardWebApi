
using Application.Core.Abstractions.Messaging;

namespace Application.Encuestas.Commands.VotarRespuesta {
    public class VotarRespuestaCommand : ICommand
    {
        public Guid EncuestaId { get; set; }
        public Guid RespuestaId { get; set; }
    }
}

