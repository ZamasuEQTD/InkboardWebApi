using Application.Categorias.Queries.GetCategorias;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/categorias")]
    public class CategoriasController : Controller
    {
        private readonly ISender _sender;

        public CategoriasController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet()]
        public async Task<IResult> Index(){
            var result = await _sender.Send(new GetCategoriasQuery());

            return result.ToResult();
        }
    }
}