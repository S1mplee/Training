using System;

public class DeposeCash : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public DeposeCash(Guid id, decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}



