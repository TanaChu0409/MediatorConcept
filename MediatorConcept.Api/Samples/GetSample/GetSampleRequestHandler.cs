using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Samples.GetSample;

internal sealed class GetSampleRequestHandler : IRequestHandler<GetSampleRequest>
{
    public Task HandlerAsync(GetSampleRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}