using Domain.Encuestas.Models.ValueObjects;

namespace Domain.Encuestas {
    public interface IEncuestasRepository
    {
        Task<Encuesta?> GetEncuestaById(EncuestaId id);
        void Add(Encuesta encuesta);
    }
}