
using Domain.Core;

namespace Domain.Encuestas.Models.ValueObjects
{
    public class VotoId : EntityId 
    {
        public VotoId(Guid id) : base( id ){}
        private VotoId(){}
    }
}