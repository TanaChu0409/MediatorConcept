namespace MediatorConcept.Api.Mediators;

public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
{
    Task HandlerAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken = default);
}

