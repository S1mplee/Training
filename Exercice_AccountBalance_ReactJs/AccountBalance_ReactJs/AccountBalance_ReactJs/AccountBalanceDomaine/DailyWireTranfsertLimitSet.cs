using ReactiveDomain.Messaging;
using System;

public class DailyWireTransfertLimitSet : Message
{
    public Guid id;
    public decimal amount;

    public DailyWireTransfertLimitSet(Guid id, decimal amount)
    {
        this.id = id;
        this.amount = amount;
    }
}