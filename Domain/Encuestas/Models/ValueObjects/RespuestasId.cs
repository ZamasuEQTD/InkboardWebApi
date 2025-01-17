
using Domain.Core;

namespace Domain.Encuestas.Models.ValueObjects
{
    public class RespuestaId  : EntityId 
    {
        public RespuestaId(Guid id) : base( id ){}
        private RespuestaId(){}
    }
}