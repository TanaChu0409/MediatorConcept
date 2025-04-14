using MediatorConcept.Api.Exceptions;
using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Behaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : PipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public override async Task<TResponse> ProcessAsync(
        TRequest request,
        Func<Task<TResponse>> next,
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