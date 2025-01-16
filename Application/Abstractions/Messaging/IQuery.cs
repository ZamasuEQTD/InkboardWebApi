
using Domain.Core;
using MediatR;

namespace Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest< TResponse>
    {
    }

}