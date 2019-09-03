using Account;
using ReactiveDomain.Messaging;
using System;


public class OverDraftlimitSet : Message
{
    public Guid id;
    public decimal amount;

    public OverDraftlimitSet(Guid id, decimal amount)
    {
        this.id = id;
        this.amount = amount;
    }
}