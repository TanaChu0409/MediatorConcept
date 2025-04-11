using MediatorConcept.Api.Domain;

namespace MediatorConcept.Api.Exceptions;

public sealed class CustomizeException(
    string requestName,
    Error? error = default,
    Exception? innerException = default)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}