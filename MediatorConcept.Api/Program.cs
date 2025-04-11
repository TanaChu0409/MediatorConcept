using MediatorConcept.Api.ApiResults;
using MediatorConcept.Api.Behaviors;
using MediatorConcept.Api.Domain;
using MediatorConcept.Api.Extensions;
using MediatorConcept.Api.Mediators;
using MediatorConcept.Api.Middleware;
using MediatorConcept.Api.Samples.GetName;
using MediatorConcept.Api.Samples.GetSample;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddTransient<IRequestHandler<GetSampleRequest>, GetSampleRequestHandler>();
builder.Services.TryAddTransient<IRequestHandler<GetNameRequest, Result<string>>, GetNameRequestHandler>();

builder.Services.TryAddTransient<IPipelineBehavior<GetSampleRequest>, RequestLoggingPipelineBehavior<GetSampleRequest>>();
builder.Services.TryAddTransient<IPipelineBehavior<GetNameRequest, Result<string>>, RequestLoggingPipelineBehavior<GetNameRequest, Result<string>>>();

builder.Services.TryAddSingleton<Mediator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/sample/{name}",
    async (string name, Mediator mediator, CancellationToken cancellationToken) =>
    {
        var request = new GetSampleRequest(name);
        await mediator.SendAsync(request, cancellationToken);
        return Results.Ok();
    })
    .WithTags("Sample");

app.MapGet("/name/{firstName}/{lastName}",
    async (string firstName, string lastName, Mediator mediator, CancellationToken cancellationToken) =>
    {
        var request = new GetNameRequest(lastName, firstName);
        var result = await mediator.SendAsync<GetNameRequest, Result<string>>(
            request,
            cancellationToken);
        return result.Match(Results.Ok, ApiResult.Problem);
    })
    .WithTags("Sample");

app.Run();