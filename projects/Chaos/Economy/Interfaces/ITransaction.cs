namespace Chaos.Economy.Interfaces;

public interface ITransaction
{
    Guid TransactionId { get; }
    PlayerId FromPlayer { get; }
    PlayerId ToPlayer { get; }
    Currency Amount { get; }
    TransactionType Type { get; }
    DateTime Timestamp { get; }
    void Execute();
}
