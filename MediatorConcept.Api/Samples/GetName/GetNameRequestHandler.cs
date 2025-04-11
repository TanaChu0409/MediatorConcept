using MediatorConcept.Api.Domain;
using MediatorConcept.Api.Mediators;

namespace MediatorConcept.Api.Samples.GetName;

internal sealed class GetNameRequestHandler :
    IRequestHandler<GetNameRequest, Result<string>>
{
    public Task<Result<string>> HandlerAsync(
        GetNameRequest request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Success($"{request.FirstName} {request.LastName}"));
    }
}