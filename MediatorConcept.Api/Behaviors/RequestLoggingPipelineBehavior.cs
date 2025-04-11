using System.Diagnostics;
using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Behaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest>(
    ILogger<RequestLoggingPipelineBehavior<TRequest>> logger)
    : IPipelineBehavior<TRequest>
    where TRequest : IRequest
{
    public async Task ProcessAsync(
        TRequest request,
        Func<Task> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        Activity.Current?.SetTag("request.name", requestName);
        logger.LogInformation("Processing request {RequestName}.", requestName);

        await next();

        logger.LogInformation("Completed request {RequestName}", requestName);
    }
}

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> ProcessAsync(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        Activity.Current?.SetTag("request.name", requestName);
        logger.LogInformation("Processing request {RequestName}.", requestName);

        Task<TResponse> nextTask = next();

        logger.LogInformation("Completed request {RequestName}", requestName);

        return await nextTask;
    }
}