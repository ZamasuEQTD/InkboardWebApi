using Domain.Categorias;
using Domain.Categorias.Models;
using Domain.Categorias.Models.ValueObjects;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly InkboardDbContext _context;

        public CategoriasRepository(InkboardDbContext context)
        {
            _context = context;
        }

        public void Add(Categoria categoria) => _context.Add(categoria);

        public Task<Subcategoria?> GetSubcategoria(SubcategoriaId subcategoriaId)
             => _context.Subcategorias.FirstOrDefaultAsync(s => s.Id == subcategoriaId);
    }
}