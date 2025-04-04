using Domain.Core;

namespace Domain.Encuestas.DomainEvents;

public class EncuestaVotadaDomainEvent : IDomainEvent {
    public Guid EncuestaId { get; set; }
    public Guid RespuestaId { get; set; }

    public EncuestaVotadaDomainEvent(Guid encuestaId, Guid respuestaId)
    {
        EncuestaId = encuestaId;
        RespuestaId = respuestaId;
    }
}