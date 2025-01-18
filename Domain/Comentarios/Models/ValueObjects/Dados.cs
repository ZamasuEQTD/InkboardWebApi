using Domain.Core;
using Domain.Core.Primitives;

namespace Domain.Comentarios.Models.ValueObjects
{
    public class Dados : ValueObject
    {
        public static readonly int MAX = 6;
        public static readonly int MIN = 1;

        public int Value { get; private set; }

        private Dados()
        {
        }

        private Dados(int value)
        {
            Value = value;
        }

        static public Dados Create(int valor)
        {
            if (ValorEsInvalido(valor)) throw new DomainBusinessException("Dados invalidos");

            return new Dados(valor);
        }

        static public bool ValorEsValido(int valor) => valor <= MAX && valor >= MIN;
        static public bool ValorEsInvalido(int valor) => !ValorEsValido(valor);
        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[]{
                this.Value
            };
        }
    }
}