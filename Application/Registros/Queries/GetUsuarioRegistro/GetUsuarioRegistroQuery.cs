using Application.Core.Abstractions.Messaging;

namespace Application.Registros.Queries.GetUsuarioRegistro
{
    public class GetUsuarioRegistroQuery : IQuery<UsuarioRegistroResponse>
    {
        public Guid UsuarioId {get;set;}
    }

    public class UsuarioRegistroResponse {
        public string? StaffName {get;set;}
        public DateTime Registrado_En {get;set;}
    }
}