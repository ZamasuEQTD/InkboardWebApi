using Domain.Core.Primitives;

namespace Domain.Core.Models
{
    public class AutorRole : ValueObject
    {
        public string Username  {get;private set;}

        public string Role  {get;private set;}
        public AutorRole(string username, string role)
        {
            Username = username;
            Role = role;
        }

        private AutorRole(){
            
        }

        protected override IEnumerable<object> GetAtomicValues() => new List<object>(){Username, Role};
    }
}