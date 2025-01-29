
using System.Text.RegularExpressions;

namespace Domain.Comentarios.Utils
{
    public static class TagUtils
    {
        static public readonly string TAG_REGEX_STRING = "[A-Z0-9]{8}";

        static private readonly string REGEX_STRING = ">>" + TAG_REGEX_STRING;

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