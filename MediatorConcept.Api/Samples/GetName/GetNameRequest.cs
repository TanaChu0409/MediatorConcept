using MediatorConcept.Api.Domain;
using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Samples.GetName;

public sealed record GetNameRequest(string LastName, string FirstName) : IRequest<Result<string>>;