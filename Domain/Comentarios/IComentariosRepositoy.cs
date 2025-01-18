using Domain.Comentarios.Models;
using Domain.Comentarios.Models.ValueObjects;

namespace Domain.Comentarios
{
    public interface IComentariosRepository
    {
        Task<Comentario?> GetComentarioById(ComentarioId id);
    }
}