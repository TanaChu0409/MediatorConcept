using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Samples.GetSample;

public sealed record GetSampleRequest(string Name) : IRequest;