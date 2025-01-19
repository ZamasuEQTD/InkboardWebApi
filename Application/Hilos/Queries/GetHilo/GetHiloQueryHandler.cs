using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Encuestas.Queries.Responses;
using Dapper;
using Domain.Comentarios;
using Microsoft.EntityFrameworkCore;

namespace Application.Hilos.Queries.GetHilo;

public class GetHiloQueryHandler : IQueryHandler<GetHiloQuery, GetHiloResponse>
{

   
    private readonly ICurrentUser _user;
    public GetHiloQueryHandler(ICurrentUser user)
    {
        _user = user;
    }

    public async Task<GetHiloResponse> Handle(GetHiloQuery request, CancellationToken cancellationToken)
    {
         

        throw new Exception("testing");
    }

}
