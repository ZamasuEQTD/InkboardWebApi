using Domain.Core.Abstractions;
using Domain.Encuestas.Models.ValueObjects;

namespace Domain.Encuestas
{
    public class Respuesta : Entity<RespuestaId>
    {
        public string Contenido { get; private set; }

        private Respuesta()
        {

        }

        public Respuesta(
            string contenido
        )
        {
            Id = new(Guid.NewGuid());
            Contenido = contenido;
        }
    }

}