
using System.Text.RegularExpressions;
using Domain.Comentarios.Models.ValueObjects;

namespace Domain.Comentarios.Utils
{
    public static class TagUtils
    {
        static private readonly string REGEX_STRING = ">>" + Tag.REGEX_STRING;

        static public List<string> GetTags(string texto)
        {
            List<string> tags = [];

            var matches = GetMatches(texto);

            foreach (Match match in matches)
            {
                tags.Add(match.Value.Substring(2));
            }

            return tags;
        }

        static public HashSet<string> GetTagsUnicos(string texto)
        {
            HashSet<string> tags = new HashSet<string>();

            var matches = GetMatches(texto);

            foreach (Match match in matches)
            {
                tags.Add(match.Value.Substring(2));
            }

            return tags;
        }

        static private MatchCollection GetMatches(string texto) => Regex.Matches(texto, REGEX_STRING);
        static public int CantidadDeTags(string texto) => GetTags(texto).Count;
        static public int CantidadDeTagsUnicos(string texto) => GetTagsUnicos(texto).Count;
    }
}