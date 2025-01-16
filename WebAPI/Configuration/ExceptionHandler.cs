using Application.Core.Exceptions;
using Domain.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Configuration
{
internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        Console.WriteLine("holaa");
        int status = exception switch
        {
            InvalidCommandException => StatusCodes.Status400BadRequest,
            DomainBusinessException => StatusCodes.Status400BadRequest,

            _ => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = status;

        var problemDetails = new ProblemDetails
        {
            Status = status,
            Title = "An error occurred",
            Type = exception.GetType().Name,
            Detail = exception.Message,
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
}