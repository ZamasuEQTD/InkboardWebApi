using Domain.Categorias.Models;
using Domain.Categorias.Models.ValueObjects;

namespace Domain.Categorias
{
    public interface ICategoriasRepository
    {
        void Add(Categoria categoria);
        Task<Subcategoria?> GetSubcategoria(SubcategoriaId subcategoriaId);
    }
}