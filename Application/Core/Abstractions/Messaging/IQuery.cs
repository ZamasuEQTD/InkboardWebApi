
using Domain.Core;
using MediatR;

namespace Application.Core.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }

}