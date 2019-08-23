using System;

public class CashDeposed : Event
{
    public  Guid AccountId ;
    public  decimal Amount;

    public CashDeposed(Guid id,decimal a)
    {
        this.AccountId = id;
        this.Amount = a;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

