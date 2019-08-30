using Account;
using System;


public class OverDraftlimitSet : Event
{
    public Guid id;
    public decimal amount;

    public OverDraftlimitSet(Guid id, decimal amount)
    {
        this.id = id;
        this.amount = amount;
    }
}