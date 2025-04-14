using System.Diagnostics;
using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Behaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : PipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public override async Task<TResponse> ProcessAsync(
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