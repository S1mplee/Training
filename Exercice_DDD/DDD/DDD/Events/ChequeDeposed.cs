using System;

public class ChequeDeposed : Event
{
    public  Guid AccountId;
    public  decimal Amount;
    public DateTime Date;
    public ChequeDeposed(Guid id, decimal amount,DateTime date)
    {
        this.AccountId = id;
        this.Amount = amount;
        this.Date = date;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

