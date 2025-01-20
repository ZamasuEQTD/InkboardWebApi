using Domain.Categorias.Models.ValueObjects;
using Domain.Comentarios.Models.ValueObjects;

namespace Domain.Comentarios.Abstractions.Services
{
    public interface IColorService
    {
        Task<Color> GenerarColor(SubcategoriaId subcategoria);
    }
}