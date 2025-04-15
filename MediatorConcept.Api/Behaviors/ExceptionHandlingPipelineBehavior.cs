using MediatorConcept.Api.Exceptions;
using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Behaviors;

public sealed class ExceptionHandlingPipelineBehavior<TRequest>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest>> logger)
    : PipelineBehavior<TRequest>
    where TRequest : IBaseRequest
{
    public override async Task<TResponse> ProcessAsync<TResponse>(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "An error occurred while processing request {RequestName}.",
                typeof(TRequest).Name);
            throw new CustomizeException(typeof(TRequest).Name, innerException: exception);
        }
    }
}