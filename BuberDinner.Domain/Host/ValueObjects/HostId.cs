using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Host.ValueObjects;

public class HostId : ValueObject
{
    public Guid Value { get; }

    private HostId(Guid value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static HostId CreateUnique(Guid value)
    {
        return new(Guid.NewGuid());
    }

    public static HostId Create(string id)
    {
        return new HostId(Guid.Parse(id));
    }
}