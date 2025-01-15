using Domain.Core.Primitives;

namespace Domain.Core
{
    public abstract class EntityId:ValueObject
    {
        public Guid Value {get;private set;}
        
        protected  EntityId(){}
        public EntityId(Guid value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }
    }
}