using System;

public class CashTransfered : Event
{
    public  Guid AccountId;
    public  decimal Amount;
    public readonly DateTime LastTransfer;

    public CashTransfered(Guid id,decimal amount,DateTime date)
    {
        this.AccountId = id;
        this.Amount = amount;
        this.LastTransfer = date;
    }

}

