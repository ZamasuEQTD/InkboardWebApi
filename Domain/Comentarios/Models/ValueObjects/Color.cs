using Domain.Core.Primitives;

namespace Domain.Comentarios.Models.ValueObjects
{
    public class Color : ValueObject
    {
        public string Value { get; private set; }

        private Color(string value)
        {
            Value = value;
        }

        static public readonly Color Multi = new Color("Multi");
        static public readonly Color Invertido = new Color("Invertido");
        static public readonly Color Rojo = new Color("Rojo");
        static public readonly Color Amarillo = new Color("Amarillo");
        static public readonly Color Azul = new Color("Azul");
        static public readonly Color Verde = new Color("Verde");
        static public readonly Color Black = new Color("Black");
        static public readonly Color White = new Color("White");

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new[] { Value };
        }
    }
}