namespace Application.Core.Abstractions
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        Guid UsuarioId { get; }
        string Username { get; }
        List<string> Roles {get;}
        bool EsModerador {get;}
    }
}