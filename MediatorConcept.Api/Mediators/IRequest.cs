namespace MediatorConcept.Api.Mediators;

public interface IRequest : IBaseRequest
{
    string Name { get; }
}

public interface IRequest<TResponse> : IBaseRequest;

public interface IBaseRequest;