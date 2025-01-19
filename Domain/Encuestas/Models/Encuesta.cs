using System.Security.Cryptography.X509Certificates;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Encuestas.Models.ValueObjects;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Encuestas
{
    public class Encuesta : Entity<EncuestaId>
    {
        public List<Respuesta> Respuestas { get; private set; }
        public List<Voto> Votos { get; private set; }
        public bool HaVotado(IdentityId id) => Votos.Any(v => v.VotanteId == id);
        public bool ContieneRespuesta(RespuestaId id) => Respuestas.Any(r => r.Id == id);

        private Encuesta() { }

        public Encuesta(List<Respuesta> respuestas) : base()
        {
            this.Id = new EncuestaId(Guid.NewGuid());
            this.Respuestas = respuestas;
            this.Votos = [];
        }

        public Result Votar(IdentityId usuarioId, RespuestaId respuestaId)
        {
            if (!ContieneRespuesta(respuestaId)) return Result.Failure(EncuestaErrors.RespuestaInexistente);

            if (HaVotado(usuarioId)) return Result.Failure(EncuestaErrors.YaVotado);

            Votos.Add(new Voto(
                usuarioId,
                respuestaId
            ));

            return Result.Success();
        }

        static public Encuesta Create(
            List<string> respuestas
        )
        {
            List<Respuesta> _respuestas = [];

            foreach (var r in respuestas)
            {
                _respuestas.Add(new Respuesta(r));
            }

            return new Encuesta(
                _respuestas
            );
        }
    }

    public static class EncuestaErrors
    {
        public static readonly Error EncuestaNoEncontrada = new("RespuestaInexistente", "La respuesta no existe.");
        public static readonly Error RespuestaInexistente = new("RespuestaInexistente", "La respuesta no existe.");
        public static readonly Error YaVotado = new("YaVotado", "Ya has votado en esta encuesta.");
    }
}