using Domain.Core;

namespace Domain.Categorias.Models.ValueObjects
{
    public class SubcategoriaId : EntityId
    {
        public SubcategoriaId(Guid id) : base(id) { }
        
        private SubcategoriaId(){}
    }
}