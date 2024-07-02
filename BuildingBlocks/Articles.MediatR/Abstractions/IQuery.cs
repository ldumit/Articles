using MediatR;

namespace Articles.MediatR;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}