using System;

public class Command : Message
{

}

public class CreateAccount : Command
{
    public readonly Guid Id;
    public readonly string _holderName;
    public readonly decimal _overdraftLimit;
    public readonly decimal _wireTransertLimit;
    public readonly decimal _cash;
    public CreateAccount(Guid id, string name, decimal d, decimal d2,decimal d3)
    {
        this.Id = id;
        this._holderName = name;
        this._overdraftLimit = d;
        this._wireTransertLimit = d2;
        this._cash = d3;
    }
}

public class DeposeCheque : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public DeposeCheque(Guid id,decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}

public class DeposeCash : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public DeposeCash(Guid id, decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}

public class WithDrawCash : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public WithDrawCash(Guid id,decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}

public class TransferCash : Command
{
    public readonly Guid accountId;
    public readonly Guid receiverId;
    public readonly decimal Amount;

    public TransferCash(Guid id,Guid id2,decimal d)
    {
        this.accountId = id;
        this.receiverId = id2;
        this.Amount = d;
    }


}



