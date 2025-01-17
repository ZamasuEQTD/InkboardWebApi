using Domain.Categorias.Models.ValueObjects;
using Domain.Core.Abstractions;

namespace Domain.Categorias.Models
{
    public class Categoria : Entity<CategoriaId>
    {
        public string Nombre { get; private set; }
        public bool OcultoPorDefecto { get; private set; }
        public ICollection<Subcategoria> Subcategorias { get; private set; } = [];

        private Categoria() { }

        public Categoria(string nombre, bool ocultoPorDefecto, List<Subcategoria> subcategorias)
        {
            this.Id = new CategoriaId(Guid.NewGuid());
            this.Nombre = nombre;
            this.OcultoPorDefecto = ocultoPorDefecto;
            this.Subcategorias = subcategorias;
        }
    }
}