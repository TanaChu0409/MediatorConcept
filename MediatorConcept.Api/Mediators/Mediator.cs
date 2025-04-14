namespace MediatorConcept.Api.Mediators;

internal sealed class Mediator(
    IServiceProvider serviceProvider)
{
    public async Task<TResponse> SendAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IRequestHandler<TRequest, TResponse>>();
        var behaviors = scope.ServiceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>();

        var pipeline = BuildPipeline(handlers, behaviors, cancellationToken);
        return await pipeline(request);
    }
    private static Func<TRequest, Task<TResponse>> BuildPipeline<TRequest, TResponse>(
        IEnumerable<IRequestHandler<TRequest, TResponse>> handlers,
        IEnumerable<IPipelineBehavior<TRequest, TResponse>> behaviors,
        CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        Func<TRequest, Task<TResponse>> handlerDelegate =
            async request =>
            {
                foreach (var handler in handlers)
                {
                    return await handler.HandlerAsync(request, cancellationToken);
                }

                throw new InvalidOperationException("No handler found for the request");
            };

        foreach (var behavior in behaviors)
        {
            var next = handlerDelegate;
            handlerDelegate = async request =>
                await behavior.ProcessAsync(
                    request,
                    () => next(request),
                    cancellationToken);
        }

        return handlerDelegate;
    }
}