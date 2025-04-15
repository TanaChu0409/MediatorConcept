using System.Diagnostics;
using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Behaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest>(
    ILogger<RequestLoggingPipelineBehavior<TRequest>> logger)
    : PipelineBehavior<TRequest>
    where TRequest : IBaseRequest
{
    public override async Task<TResponse> ProcessAsync<TResponse>(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
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