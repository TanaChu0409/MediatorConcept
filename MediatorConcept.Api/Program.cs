using MediatorConcept.Api;
using MediatorConcept.Api.ApiResults;
using MediatorConcept.Api.Domain;
using MediatorConcept.Api.Extensions;
using MediatorConcept.Api.Mediators;
using MediatorConcept.Api.Middleware;
using MediatorConcept.Api.Samples.GetName;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPipelineBehaviors(AssemblyReference.Assembly);

builder.Services.TryAddScoped<IRequestHandler<GetNameRequest, Result<string>>, GetNameRequestHandler>();

builder.Services.TryAddScoped<Mediator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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