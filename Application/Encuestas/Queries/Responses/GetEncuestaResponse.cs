using System.Text.Json.Serialization;

namespace Application.Encuestas.Queries.Responses
{
    public class GetEncuestaResponse
    {
        public Guid Id { get; set; }
        public List<GetEncuestaRespuestaResponse> Respuestas { get; set; } = [];
        [JsonPropertyName("respuesta_votada")]
        public Guid? RespuestaVotada { get; set; }
    }
    
    public class GetEncuestaRespuestaResponse
    {
        public Guid Id { get; set; }
        public string Respuesta { get; set; }
        public int Votos { get; set; }
    }
}