using System;

public class CashWithdrawn : Event
{
    public  Guid AccountId;
    public  decimal Amount;

    public CashWithdrawn(Guid id, decimal amount)
    {
        this.AccountId = id;
        this.Amount = amount;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

