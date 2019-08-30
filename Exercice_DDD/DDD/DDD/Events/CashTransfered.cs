using System;

public class CashTransfered : Event
{
    public  Guid AccountId;
    public  decimal Amount;

    public CashTransfered(Guid id,decimal amount)
    {
        this.AccountId = id;
        this.Amount = amount;
    }

}

