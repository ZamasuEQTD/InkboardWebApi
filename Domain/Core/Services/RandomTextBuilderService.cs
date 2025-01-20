using System.Text;

namespace Domain.Core.Services
 {
    static public class RandomTextBuilderService
    {
        private static readonly string _caracteres_string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static readonly List<char> caracteres = _caracteres_string.ToList();
        static public string BuildRandomString(Random random, int length)
        {
            StringBuilder builder = new();

            for (int i = length - 1; i >= 0; i--)
            {
                int randomIndex = random.Next(caracteres.Count);

                builder.Append(caracteres[randomIndex]);
            }
            return builder.ToString();
        }
    }

 }