using System;

public class DeposeCheque : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public DeposeCheque(Guid id,decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}



