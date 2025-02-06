using Application.Notificaciones.Commands.LeerNotificacion;
using Application.Notificaciones.Commands.LeerNotificaciones;
using Application.Notificaciones.Queries.GetNotificacionesQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {

    [Route("api/notificaciones")]
    public class NotificacionesController : Controller
    {

        private readonly ISender _sender;

        public NotificacionesController(ISender sender)
        {
            _sender = sender;
        }

 
        [Authorize]
        [HttpGet()]
        [ProducesResponseType(typeof(List<GetNotificacionResponse>), 200)]
        public async Task<IResult> Index([FromQuery(Name = "ultima_notificacion")] Guid? ultimaNotificacion) {
            var result = await _sender.Send(new GetNotificacionesQuery(){
                UltimaNotificacion = ultimaNotificacion
            });

            return result.ToResult();
        }

        [Authorize]
        [HttpPost("{id}/leer")]
        public async Task<IResult> LeerNotificacion(Guid id) {
            var result = await _sender.Send(new LeerNotificacionCommand(){
                Id = id
            });

            return result.ToResult();
        }

        [Authorize]
        [HttpPost("leer")]
        public async Task<IResult> LeerNotificaciones(){
             var result = await _sender.Send(new LeerNotificacionesCommand(){
            });

            return result.ToResult();
        }
    }
}