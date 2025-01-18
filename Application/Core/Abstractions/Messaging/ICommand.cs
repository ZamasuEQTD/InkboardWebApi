using Domain.Core;
using MediatR;

namespace Application.Core.Abstractions.Messaging
{
    public interface ICommand : IRequest
    {

    }
    public interface ICommand<TResponse> : IRequest<TResponse>
    {

    }
}