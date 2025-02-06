using Application.Core.Abstractions.Messaging;

namespace Application.Notificaciones.Queries.GetNotificacionesQuery
{
    public class GetNotificacionesQuery : IQuery<List<GetNotificacionResponse>>
    {
        public Guid? UltimaNotificacion {get;set;}
    }

    public class GetNotificacionResponse {
        public string Tipo {get;set;}
        public Guid Id {get;set;}
        public string? Comentario_Respuesta_Tag {get;set;}
        public DateTime Fecha {get;set;}
        public string Contenido {get;set;}
        public GetHiloNotificacionResponse Hilo {get;set;}
        public string? Comentario_Respondido_Tag {get;set;}

    }

    public class GetHiloNotificacionResponse
    {
        public Guid Id {get;set;}
        public string Titulo {get;set;}
        public string Portada {get;set;}
    }
}