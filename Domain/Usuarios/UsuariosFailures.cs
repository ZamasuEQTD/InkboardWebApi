
using Domain.Core;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Usuarios
{
    public static class UsuarioErrors
    {
        public static readonly Error UsernameOcupado = new Error("Usuarios.UsernameOcupado", "El nombre de usuario ya está en uso.");
        public static readonly Error UsernameTieneEspaciosEnBlanco = new Error("Usuarios.UsernameTieneEspaciosEnBlanco", "El nombre de usuario no puede contener espacios en blanco.");
        public static readonly Error LongitudDeUsernameInvalida = new Error("Usuarios.LongitudDeUsernameInvalida", $"El nombre de usuario debe tener entre {Username.MIN_LENGTH} y {Username.MAXIMO_LENGTH} caracteres.");
        public static readonly Error LongitudDePasswordInvalida = new Error("Usuarios.LongitudDePasswordInvalida", $"La contraseña debe tener entre {Password.MIN_LENGTH} y {Password.MAXIMO_LENGTH} caracteres.");
        public static readonly Error PasswordTieneEspaciosEnBlanco = new Error("Usuarios.PasswordTieneEspaciosEnBlanco", "La contraseña no puede contener espacios en blanco.");
        public static readonly Error CredencialesInvalidas = new Error("Usuarios.CredencialesInvalidas", "El nombre de usuario o la contraseña son incorrectos.");
    }
}