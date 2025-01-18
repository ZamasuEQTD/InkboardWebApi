using System.Text.RegularExpressions;
using Domain.Core;
using Domain.Core.Primitives;

namespace Domain.Comentarios.Models.ValueObjects
{
    public class TagUnico : ValueObject
    {
        static public readonly int Lenght = 3;
        static public readonly string RegexExp = "^[A-Z0-9]{3}$";
        public string Value { get; private set; }
        private TagUnico()
        {
        }

        private TagUnico(string value)
        {
            Value = value;
        }

        public static TagUnico Create(string tag)
        {
            if (!EsTagValido(tag))  throw new DomainBusinessException("Tag unico invalido");

            return new TagUnico(tag);
        }

        static public bool EsTagValido(string tag) => Regex.IsMatch(tag, RegexExp);

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }

    }
}