using Application.Core.Abstractions.Messaging;
using Application.Registros.Queries.GetHilosPosteadosRegistros;

namespace Application.Registros.Queries.GetComentariosRegistros
{
    public class GetComentariosRegistrosQuery :IQuery<List<RegistroResponse>>{
        public Guid UsuarioId {get;set;}
        public Guid? UltimoComentario {get;set;}
    }
}
