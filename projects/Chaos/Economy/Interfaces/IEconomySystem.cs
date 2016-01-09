namespace Chaos.Economy.Interfaces;

public interface IEconomySystem
{
    Currency GetBalance(PlayerId playerId);
    void AddCurrency(PlayerId playerId, Currency amount);
    bool DeductCurrency(PlayerId playerId, Currency amount);
    void ApplyInvisibleTax(PlayerId playerId, Currency transactionAmount);
}
