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

        static public Result<Texto> Create(string value)
        {
            if (value.Length < MIN || value.Length > MAX) return TextoErrors.LongitudInvalida;

            if (TagUtils.CantidadDeTags(value) > 5)  return  TextoErrors.MaxTagsSuperado;

            return new Texto(value);
        }

        protected override IEnumerable<object> GetAtomicValues() => [
            this.Value
        ];
    }

    public static class TextoErrors
    {
        public static readonly Error LongitudInvalida = new("LongitudInvalida", "Longitud de texto invalida.");
        public static readonly Error MaxTagsSuperado = new("MaxTagsSuperado", "Has superado la maxima cantidad de taggueos permitida.");
    }
}


