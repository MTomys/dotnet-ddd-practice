using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menu.ValueObjects;

public class AverageRating : ValueObject
{
    public decimal Value { get; set; }
    public int NumRatings { get; set; }
    
    public AverageRating(decimal value, int numRatings)
    {
        Value = value;
        NumRatings = numRatings;
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return NumRatings;
    }
}