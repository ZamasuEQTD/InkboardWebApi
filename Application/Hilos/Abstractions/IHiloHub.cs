using Application.Comentarios.Queries.GetComentarios;

namespace Application.Hilos.Abstractions
{
    public interface IHiloHub
    {
        Task NotificarHiloComentado(Guid hiloId, GetComentarioResponse comentario);
        Task NotificarComentarioEliminado(Guid hiloId, string comentarioTag);
    }
}