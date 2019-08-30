using System;

public class ChequeDeposed : Event
{
    public readonly Guid AccountId;
    public readonly decimal Amount;
    public readonly DateTime DepositDate;
    public readonly DateTime ClearDate;
    public ChequeDeposed(Guid id, decimal amount, DateTime date, DateTime clear)
    {
        this.AccountId = id;
        this.Amount = amount;
        this.DepositDate = date;
        this.DepositDate = clear;
    }
}

