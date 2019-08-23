using System;

public class TransferCash : Command
{
    public readonly Guid accountId;
    public readonly Guid receiverId;
    public readonly decimal Amount;

    public TransferCash(Guid id,Guid id2,decimal d)
    {
        this.accountId = id;
        this.receiverId = id2;
        this.Amount = d;
    }


}



