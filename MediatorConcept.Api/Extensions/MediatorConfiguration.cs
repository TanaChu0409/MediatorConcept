using System.Reflection;
using MediatorConcept.Api.Mediators;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatorConcept.Api.Extensions;
public static class MediatorConfiguration
{
    public static IServiceCollection AddPipelineBehaviors(
        this IServiceCollection services,
        Assembly assembly)
    {
        Type[] pipelines = assembly
            .GetTypes()
            .Where(t =>
                t.IsAssignableTo(typeof(IPipelineBehavior)) &&
                !t.IsInterface &&
                !t.IsAbstract)
            .ToArray();

        foreach (Type pipeline in pipelines)
        {
            services.TryAddScoped(pipeline);

            Type pipelineRequest = pipeline
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedDecorator = typeof(PipelineBehaviorDecorator<>)
                .MakeGenericType(pipelineRequest);

            services.Decorate(pipeline, closedDecorator);
        }

        return services;
    }
}