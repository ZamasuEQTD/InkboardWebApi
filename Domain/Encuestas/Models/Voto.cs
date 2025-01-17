using Domain.Core.Abstractions;
using Domain.Encuestas.Models.ValueObjects;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Encuestas
{
    public class Voto : Entity<VotoId>
    {
        public IdentityId VotanteId { get; private set; }
        public RespuestaId RespuestaId { get; private set; }

        private Voto() { }

        public Voto(IdentityId votanteId, RespuestaId respuestaId) : base()
        {
            Id = new(Guid.NewGuid());
            VotanteId = votanteId;
            RespuestaId = respuestaId;
        }
    }
}