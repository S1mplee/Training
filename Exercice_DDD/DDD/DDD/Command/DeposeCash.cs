using Commands;
using System;

public class DeposeCash : Command
{
    public readonly Guid AccountId;
    public readonly decimal Amount;

    public DeposeCash(Guid id, decimal amount)
    {
        this.AccountId = id;
        this.Amount = amount;
    }
}



