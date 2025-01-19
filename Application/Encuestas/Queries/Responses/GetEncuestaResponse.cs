using System.Text.Json.Serialization;

namespace Application.Encuestas.Queries.Responses
{
    public class GetEncuestaResponse
    {
        public Guid Id { get; set; }
        public List<GetEncuestaRespuestaResponse> Respuestas { get; set; } = [];
        public Guid? Respuesta_votada { get; set; }
    }
    
    public class GetEncuestaRespuestaResponse
    {
        public Guid Id { get; set; }
        public string Respuesta { get; set; }
        public int Votos { get; set; }
    }
}