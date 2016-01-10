using Chaos.Economy.ValueObjects;

namespace Chaos.Economy.Interfaces;

public interface IInvisibleTax
{
    double TaxRate { get; }
    Currency CalculateTax(Currency amount);
    Currency Apply(Currency amount);
}
