namespace Domain.Core
{
public static class AppRoles{
        public static readonly string Owner = "Owner";
        public static readonly string Moderador = "Moderador";
        public static readonly string Anonimo = "Anonimo";
        public static readonly List<string> StaffRoles = [Owner,Moderador ];
        public static readonly List<string> Roles = [..StaffRoles, Anonimo];
    }
}