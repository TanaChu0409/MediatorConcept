namespace MediatorConcept.Api.Mediators;

public abstract class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public abstract Task<TResponse> ProcessAsync(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default);

    public Task<TResponse> ProcessAsync(
        IRequest<TResponse> request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default)
    {
        return ProcessAsync((TRequest)request, next, cancellationToken);
    }
}