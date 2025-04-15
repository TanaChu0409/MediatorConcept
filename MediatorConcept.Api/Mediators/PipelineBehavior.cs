namespace MediatorConcept.Api.Mediators;

public abstract class PipelineBehavior<TRequest>
    : IPipelineBehavior<TRequest>
    where TRequest : IBaseRequest
{
    public abstract Task<TResponse> ProcessAsync<TResponse>(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default);

    public Task<TResponse> ProcessAsync<TResponse>(
        IRequest<TResponse> request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        return ProcessAsync((TRequest)request, next, cancellationToken);
    }
}