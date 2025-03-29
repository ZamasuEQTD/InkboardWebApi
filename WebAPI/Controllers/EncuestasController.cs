using Application.Encuestas.Commands.VotarRespuesta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace WebAPI.Controllers {

[Route("api/encuestas")]
public class EncuestasController : Controller
{
    private readonly ISender _sender;

    public EncuestasController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpPost("votar/encuesta/{encuesta:guid}/respuesta/{respuesta:guid}")]
    public async Task<IResult> Votar(Guid encuesta, Guid respuesta)
    {
        var command = new VotarRespuestaCommand(){
            EncuestaId = encuesta,
            RespuestaId = respuesta
        };

        var result =  await _sender.Send(command);

        return  result.ToResult();
    }
}
}
