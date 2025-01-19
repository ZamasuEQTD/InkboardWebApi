using Domain.Core;

namespace Domain.Baneos
{
    static public class BaneoErrors
    {
        static public readonly Error NoEsAnonimo= new ("UsuarioNoEsAnonimo", "Solamente puedes banear usuarios anonimos");
    }
}