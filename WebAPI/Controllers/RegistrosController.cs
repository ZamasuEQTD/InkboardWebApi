using Application.Registros.Queries.GetComentariosRegistros;
using Application.Registros.Queries.GetHilosPosteadosRegistros;
using Application.Registros.Queries.GetUsuarioRegistro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/registros")]
    public class RegistrosController : ControllerBase
    {

        private readonly ISender _sender;

        public RegistrosController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("hilos-posteados/usuario/{usuario:guid}")]
        public async Task<IResult> GetHilosPosteados(Guid usuario, [FromQuery(Name = "ultimo_hilo")] Guid? ultimoHilo)
        {
            var result = await _sender.Send(new GetHilosPosteadosRegistrosQuery()
            {
                UsuarioId = usuario,
                Hilo = ultimoHilo
            });

            return result.ToResult();
        }
        [HttpGet("comentarios/usuario/{usuario:guid}")]
        public async Task<IResult> GetComentarios(Guid usuario, [FromQuery(Name = "ultimo_comentario")] Guid? ultimoComentario)
        {
            var result = await _sender.Send(new GetComentariosRegistrosQuery()
            {
                UsuarioId = usuario,
                UltimoComentario = ultimoComentario
            });

            return result.ToResult();
        }

        [HttpGet("usuario/{usuario:guid}")]
        public async Task<IResult> GetRegistroDeUsuario(Guid usuario)
        {
            var result = await _sender.Send(new GetUsuarioRegistroQuery()
            {
                UsuarioId = usuario
            });

            return result.ToResult();
        }
    }
}