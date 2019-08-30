using Commands;
using System;

public class WithDrawCash : Command
{
    public readonly Guid AccountId;
    public readonly decimal Amount;

    public WithDrawCash(Guid id,decimal amount)
    {
        this.AccountId = id;
        this.Amount = amount;
    }
}



