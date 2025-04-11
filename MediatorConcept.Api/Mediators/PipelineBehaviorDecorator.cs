namespace MediatorConcept.Api.Mediators;

public sealed class PipelineBehaviorDecorator<TRequest, TResponse>(
    IPipelineBehavior<TRequest, TResponse> innerPipe,
    IPipelineBehavior<TRequest, TResponse> nextPipe)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> ProcessAsync(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default)
    {
        return await innerPipe.ProcessAsync(
            request,
            () => nextPipe.ProcessAsync(request, next, cancellationToken),
            cancellationToken);
    }
}