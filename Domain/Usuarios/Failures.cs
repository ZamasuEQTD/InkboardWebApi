using Domain.Core;

namespace Domain.Usuarios {
    static public class UsuariosFailures
    {
        public static readonly Error CredencialesInvalidas = new Error("Usuarios.CredencialesInvalidas", "El nombre de usuario o la contraseña son incorrectos.");
    }
}