using Commands;
using System;

public class TransferCash : Command
{
    public readonly Guid AccountId;
    public readonly decimal Amount;
    public readonly DateTime TransferDate;

    public TransferCash(Guid id,decimal Amount,DateTime date)
    {
        this.AccountId = id;
        this.Amount = Amount;
        this.TransferDate = date;
    }


}



