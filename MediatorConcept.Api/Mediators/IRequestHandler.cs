namespace MediatorConcept.Api.Mediators;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken = default);
}