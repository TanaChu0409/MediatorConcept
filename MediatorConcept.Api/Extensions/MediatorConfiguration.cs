namespace MediatorConcept.Api.Extensions;

public sealed class MediatorConfiguration
{
    private readonly Dictionary<Type, List<object>> _services = [];

    public void Register<TService>(TService implementation)
    {
        var type = typeof(TService);
        if (_services.TryGetValue(type, out List<object>? value))
        {
            _services[type] = value;
        }
        else
        {
            value = ([]);
        }
    }
}