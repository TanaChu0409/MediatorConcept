namespace MediatorConcept.Api.Mediators;

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

public interface IPipelineBehavior
{
    Task<TResponse> ProcessAsync<TResponse>(
        IRequest<TResponse> request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default);
}

public interface IPipelineBehavior<in TRequest> : IPipelineBehavior
    where TRequest : IBaseRequest
{
    Task<TResponse> ProcessAsync<TResponse>(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default);
}