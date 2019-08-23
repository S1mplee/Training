using System;

public class CashTransfered : Event
{
    public  Guid AccountId;
    public  Guid receiverId;
    public  decimal Amount;

    public CashTransfered(Guid id,Guid id2,decimal d)
    {
        this.AccountId = id;
        this.receiverId = id2;
        this.Amount = d;
    }
    public override string ToString()
    {
        return "" + AccountId + " " + Amount + " "+receiverId;
    }

}

