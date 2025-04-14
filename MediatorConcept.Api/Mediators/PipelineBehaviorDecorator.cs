
namespace MediatorConcept.Api.Mediators;

internal sealed class PipelineBehaviorDecorator<TRequest, TResponse>(
    IPipelineBehavior<TRequest, TResponse> decorated)
    : PipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public override Task<TResponse> ProcessAsync(
        TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
