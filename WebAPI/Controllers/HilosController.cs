using Application.Hilos.Commands;
using Application.Hilos.Commands.CambiarNotificaciones;
using Application.Hilos.Commands.DenunciarHilo;
using Application.Hilos.Commands.EliminarHilo;
using Application.Hilos.Commands.EliminarSticky;
using Application.Hilos.Commands.EstablecerSticky;
using Application.Hilos.Commands.OcultarHilo;
using Application.Hilos.Commands.PonerHiloEnFavorito;
using Application.Hilos.Commands.PostearHilo;
using Application.Hilos.Commands.SeguirHilo;
using Application.Hilos.Queries.GetHilo;
using Application.Hilos.Queries.GetPortadas;
using Infraestructure.Media;
using Infraestructure.Services.Providers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/hilos")]
    public class HilosController : Controller
    {

        private readonly ISender sender;

        public HilosController(ISender sender)
        {
            this.sender = sender;
        }

        [Authorize]
        [HttpPost("postear")]
        public async Task<IResult> Postear([FromForm] PostearHiloRequest request)
        {
            var result = await sender.Send(new PostearHiloCommand()
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Spoiler = request.Spoiler,
                Subcategoria = request.Subcategoria,
                DadosActivados = request.DadosActivados,
                IdUnicoAtivado = request.IdUnicoActivado,
                Encuesta = request.Encuesta ?? [],
                File = request.File is not null ? new FormFileImplementation(request.File) : null,
                Embed = request.Embed is not null ? new EmbedFile(request.Embed) : null
            });

            Console.Write(result);

            return result.ToResult();
        }

        [Authorize(Roles = "Moderador")]
        [HttpDelete("eliminar/{hilo:guid}")]
        public async Task<IResult> Eliminar(Guid hilo)
        {
            await sender.Send(new EliminarHiloCommand(){
                Hilo = hilo
            });
 
            return     Results.NoContent();
        }

        [Authorize(Roles = "Moderador")]
        [HttpPost("establecer-sticky/{hilo:guid}")]
        public async Task<IResult> EstablecerSticky(Guid hilo)
        {
            await sender.Send(new EstablecerStickyCommand(){
                Hilo = hilo
            });

            return Results.NoContent();
        }

        [Authorize(Roles = "Moderador")]
        [HttpDelete("eliminar-sticky/{hilo:guid}")]
        public async Task<IResult> EliminarSticky(Guid hilo)
        {
            await sender.Send(new EliminarStickyCommand(){
                Hilo = hilo
            });

            return  Results.Ok();
        }


        [Authorize]
        [HttpPost("denunciar/{hilo:guid}")]
        public async Task<IResult> Denunciar(Guid hilo)
        {
            await sender.Send(new DenunciarHiloCommand(){
                Hilo = hilo
            });

            return  Results.Ok();
                 

        }

        [Authorize]
        [HttpPost("colecciones/seguidos/seguir/{hilo:guid}")]
        public async Task<IResult> Seguir(Guid hilo)
        {
            await sender.Send(
                new SeguirHiloCommand()
                {
                    Hilo = hilo
                }
            );

            return Results.Ok();
        }

        [HttpPost("colecciones/ocultos/ocultar/{hilo:guid}")]
        public async Task<IResult> Ocultar(Guid hilo)
        {
            await sender.Send(new OcultarHiloCommand()
            {
                Hilo = hilo
            });

            return Results.Ok();
        }

        [Authorize]
        [HttpPost("colecciones/favoritos/poner-en-favoritos/{hilo:guid}")]
        public async Task<IResult> PonerEnFavoritos(Guid hilo)
        {
            await sender.Send(new PonerHiloEnFavoritoCommand()
            {
                Hilo = hilo
            });

            return Results.Ok();
        }

        [Authorize]
        [HttpPost("cambiar-notificaciones/{hilo:guid}")]
        public async Task<IResult> CambiarNotificaciones(Guid hilo)
        {
            await sender.Send(new CambiarNotificacionesHiloCommand()
            {
                HiloId = hilo
            });

            return Results.Ok();
        }
    
    
         [HttpGet("{hiloId:guid}")]
         public async Task<IResult> GetHilo(Guid hiloId)
         {
            var result =  await sender.Send(new GetHiloQuery()
            {
                HiloId = hiloId
            });

             return result.ToResult();
         }


        [HttpGet()]
        public async Task<IResult> Index([FromQuery] GetPortadasRequest request){
            
            var result = await sender.Send(new GetPortadasQuery(){
                Categoria = request.Categoria,
                Titulo = request.Titulo,
                UltimaPortada = request.UltimaPortada,
                CategoriasBloqueadas = request.CategoriasBloqueadas 
            });
            return result.ToResult();
        }
    }
    public class PostearHiloRequest
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Guid Subcategoria { get; set; }
        public IFormFile? File { get; set; }
        public string? Embed { get; set; }
        public List<string>? Encuesta { get; set; }
        public bool Spoiler { get; set; }
        public bool DadosActivados { get; set; }
        public bool IdUnicoActivado { get; set; }
    }

    public class GetPortadasRequest
    {
        public Guid? Categoria { get; set; }
        public string? Titulo { get; set; }
        public Guid? UltimaPortada { get; set; }
        public List<Guid> CategoriasBloqueadas { get; set; } = [];
    }
}