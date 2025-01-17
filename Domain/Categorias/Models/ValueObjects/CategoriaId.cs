using Domain.Core;

namespace Domain.Categorias.Models.ValueObjects
{
    public class CategoriaId : EntityId
    {
        public CategoriaId(Guid id) : base(id) { }
        
        private CategoriaId(){}
    }
}