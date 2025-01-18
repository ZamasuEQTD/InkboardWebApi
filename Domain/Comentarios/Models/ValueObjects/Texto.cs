
using Domain.Comentarios.Utils;
using Domain.Core;
using Domain.Core.Primitives;

namespace Domain.Comentarios.Models.ValueObjects
{
    public class Texto : ValueObject
    {
        static public readonly int MAX = 255;
        static public readonly int MIN = 5;

        public string Value { get; private set; }
        public Texto() { }
        private Texto(string value)
        {
            Value = value;
        }

        static public Texto Create(string value)
        {
            if (value.Length < MIN || value.Length > MAX) throw new DomainBusinessException("Longitud de texto invalida");

            if (TagUtils.CantidadDeTags(value) > 5) throw new DomainBusinessException("Has superado la maxima cantidad de taggueos permitida.");

            return new Texto(value);
        }

        protected override IEnumerable<object> GetAtomicValues() => [
            this.Value
        ];
    }
}


