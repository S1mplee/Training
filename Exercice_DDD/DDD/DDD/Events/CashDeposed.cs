using System;

public class CashDeposed : Event
{
    public  Guid AccountId ;
    public  decimal Amount;

    public CashDeposed(Guid id,decimal amount)
    {
        this.AccountId = id;
        this.Amount = amount;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

