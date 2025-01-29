using System.Text.Json.Serialization;
using Application.Comentarios;
using Application.Comentarios.Commands;
using Application.Comentarios.Commands.ComentarHilo;
using Application.Comentarios.Commands.DestacarComentario;
using Application.Comentarios.Commands.EliminarComentari;
using Application.Comentarios.Queries.GetComentarios;
using Infraestructure.Media;
using Infraestructure.Services.Providers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebAPI.Controllers;

namespace WebApi.Controllers
{
    
    [Route("api/comentarios")]
    public class ComentariosController : Controller
    {

        private readonly ISender _sender;

        public ComentariosController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("hilo/{hilo:guid}")]
        public async Task<IResult> GetComentarios(Guid hilo)
        {
            var result = await _sender.Send(new GetComentariosQuery()
            {
                Hilo = hilo
            });

            return result.ToResult();
        }


        [Authorize]
        [HttpPost("hilo/{hilo:guid}/destacar/{comentario:guid}")]
        public async Task<IResult> Destacar(Guid hilo, Guid comentario)
        {
            var result = await _sender.Send(new DestacarComentarioCommand(
                hilo,
                comentario
            ));

            return result.ToResult();
        }

        [Authorize]
        [HttpPost("comentar-hilo/{hilo:guid}")]
        public async Task<IResult> Comentar(
            [FromRoute] Guid hilo,
            [FromForm] ComentarHiloRequest request
        )
        {
            var result = await _sender.Send(new ComentarHiloCommand(
                hilo,
                request.Texto,
                request.EmbedFile is not null ? new EmbedFile(request.EmbedFile!) : null,
                request.File is not null ? new FormFileImplementation(request.File!) : null,
                request.Spoiler
            ));

            return result.ToResult();
        }

        [Authorize(Roles = "Moderador")]
        [HttpDelete("eliminar/hilo/{hilo:guid}/comentario/{comentario:guid}")]
        public async Task<IResult> Eliminar(Guid comentario, Guid hilo)
        {
            var result = await _sender.Send(new EliminarComentarioCommand(){
                Comentario = comentario,
                Hilo = hilo
            });

            return result.ToResult();
        }
    }

    public class ComentarHiloRequest
    {
        [JsonPropertyName("texto")]
        public string Texto { get; set; }
        [JsonPropertyName("embed_file")]
        public string? EmbedFile { get; set; }
        [JsonPropertyName("file")]
        public IFormFile? File { get; set; }
        [JsonPropertyName("spoiler")]
        public bool Spoiler {get;set;}
    }
}