using System;

public class CashWithdrawn : Event
{
    public  Guid AccountId;
    public  decimal Amount;

    public CashWithdrawn(Guid id, decimal a)
    {
        this.AccountId = id;
        this.Amount = a;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

