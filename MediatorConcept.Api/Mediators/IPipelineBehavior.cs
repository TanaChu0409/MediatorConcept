namespace MediatorConcept.Api.Mediators;

public interface IPipelineBehavior<TResponse>
{
    Task<TResponse> ProcessAsync(
        IRequest<TResponse> request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default);
}

public interface IPipelineBehavior<in TRequest, TResponse> : IPipelineBehavior<TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    Task<TResponse> ProcessAsync(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default);
}