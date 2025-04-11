﻿namespace MediatorConcept.Api.Mediators;

public interface IPipelineBehavior<in TRequest>
    where TRequest : notnull
{
    Task ProcessAsync(
        TRequest request,
        Func<Task> next,
        CancellationToken cancellationToken = default);
}

public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : notnull
{
    Task<TResponse> ProcessAsync(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default);
}