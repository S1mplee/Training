using Account;
using System;

public class DailyWireTransfertLimitSet : Event
{
    public Guid id;
    public decimal amount;

    public DailyWireTransfertLimitSet(Guid id, decimal amount)
    {
        this.id = id;
        this.amount = amount;
    }
}
