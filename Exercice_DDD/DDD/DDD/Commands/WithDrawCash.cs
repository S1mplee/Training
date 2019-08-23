using System;

public class WithDrawCash : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public WithDrawCash(Guid id,decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}



