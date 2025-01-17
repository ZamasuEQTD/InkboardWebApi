using Domain.Categorias.Models.ValueObjects;
using Domain.Core.Abstractions;

namespace Domain.Categorias.Models
{
    public class Subcategoria : Entity<SubcategoriaId>
    {
        public string Nombre { get; private set; }
        public string NombreCorto { get; private set; }
        private Subcategoria() { }
        public Subcategoria(string nombre, string nombreCorto)
        {
            Id = new SubcategoriaId(Guid.NewGuid());
            Nombre = nombre;
            NombreCorto = nombreCorto;
        }
        public bool EsParanormal => Nombre == "Paranormal";
    }

}