using Application.Baneos.Commands.BanearUsuario;
using Application.Baneos.Commands.DesbanearUsuario;
using Domain.Baneos.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/baneos")]
    public class BaneosController : ControllerBase
    {

        private readonly ISender _sender;

        public BaneosController(ISender sender)
        {
            _sender = sender;
        }


        [Authorize(Roles = "Moderador")]
        [HttpPost("banear/{usuario:guid}")]
        public async Task<IResult> Banear(Guid usuario, [FromBody] BanearUsuarioRequest baneo)
        {
            var result =await _sender.Send(new BanearUsuarioCommand()
            {
                UsuarioId = usuario,
                Duracion = baneo.Duracion,
                Razon = baneo.Razon,
                Mensaje = baneo.Mensaje
            });

            return result.ToResult();
        }
                
        [Authorize(Roles = "Moderador")]
        [HttpPost("desbanear/{usuario:guid}")]
        public async Task<IResult> Desbanear(Guid usuario)
        {
            var result =await _sender.Send(new DesbanearUsuarioCommand()
            {
                Usuario = usuario,
            });

            return result.ToResult();
        }
    }


    public class BanearUsuarioRequest
    {
        public BaneoRazon Razon { get; set; }
        public DuracionBaneo Duracion { get; set; }
        public string? Mensaje { get; set; }
    }
}