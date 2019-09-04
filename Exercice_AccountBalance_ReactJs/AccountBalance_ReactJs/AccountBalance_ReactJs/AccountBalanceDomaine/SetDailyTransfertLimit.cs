using ReactiveDomain.Messaging;
using System;


public class SetDailyTransfertLimit : Command
{
    public Guid id;
    public decimal amount;

    public SetDailyTransfertLimit(Guid id, decimal amount)
    {
        this.id = id;
        this.amount = amount;
    }
}