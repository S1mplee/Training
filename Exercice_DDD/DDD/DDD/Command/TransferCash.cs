using Commands;
using System;

public class TransferCash : Command
{
    public readonly Guid AccountId;
    public readonly decimal Amount;

    public TransferCash(Guid id,decimal Amount)
    {
        this.AccountId = id;
        this.Amount = Amount;
    }


}



