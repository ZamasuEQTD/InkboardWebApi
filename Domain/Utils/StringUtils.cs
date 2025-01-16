using System.Text.RegularExpressions;

namespace Domain.Utils
{
    public static class StringUtils {
        public static bool ContieneEspaciosEnBlanco(string value) {
            // Verifica si el valor proporcionado es nulo o vacío
            if (string.IsNullOrEmpty(value)) {
                return false;
            }

            // Utiliza una expresión regular para verificar espacios en blanco
            return Regex.IsMatch(value, @"\s");
        }
    }
}