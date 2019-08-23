using System;

public class ChequeDeposed : Event
{
    public  Guid AccountId;
    public  decimal Amount;

    public ChequeDeposed(Guid id, decimal a)
    {
        this.AccountId = id;
        this.Amount = a;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

