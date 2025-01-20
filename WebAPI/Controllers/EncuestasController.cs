using Application.Encuestas.Commands.VotarRespuesta;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebApi.Controllers;

[ApiController]
[Route("api/encuestas")]
public class EncuestasController : ControllerBase
{
    private readonly ISender _sender;

    public EncuestasController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpPost("/votar/encuesta/{encuesta:guid}/respuesta/{respuesta:guid}")]
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