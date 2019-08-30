using Commands;
using System;

public class DeposeCheque : Command
{
    public readonly Guid AccountId;
    public readonly decimal Amount;
    public readonly DateTime DepositDate;
    public DeposeCheque(Guid id,decimal amount,DateTime date)
    {
        this.AccountId = id;
        this.Amount = amount;
        this.DepositDate = date;
    }
}



