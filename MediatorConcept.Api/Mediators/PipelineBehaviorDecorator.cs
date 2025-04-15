namespace MediatorConcept.Api.Mediators;

internal sealed class PipelineBehaviorDecorator<TRequest>(
    IPipelineBehavior<TRequest> decorated)
    : PipelineBehavior<TRequest>
    where TRequest : IBaseRequest
{
    public override async Task<TResponse> ProcessAsync<TResponse>(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        return await decorated.ProcessAsync(
            request,
            next,
            cancellationToken);
    }
}
