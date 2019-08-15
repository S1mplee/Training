using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Command
{

}

public class AccountCreate : Command
{
    public readonly Guid Id;
    public readonly string _holderName;
    public readonly decimal _overdraftLimit;
    public readonly decimal _wireTransertLimit;
    public readonly decimal _cash;
    public AccountCreate(Guid id, string name, decimal d, decimal d2,decimal d3)
    {
        this.Id = id;
        this._holderName = name;
        this._overdraftLimit = d;
        this._wireTransertLimit = d2;
        this._cash = d3;
    }
}

public class ChequeDepose : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public ChequeDepose(Guid id,decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}

public class CashDepose : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public CashDepose(Guid id, decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}

public class CashWithdraw : Command
{
    public readonly Guid accountId;
    public readonly decimal Amount;

    public CashWithdraw(Guid id,decimal amount)
    {
        this.accountId = id;
        this.Amount = amount;
    }
}

public class CashTransfer : Command
{
    public readonly Guid accountId;
    public readonly Guid receiverId;
    public readonly decimal Amount;

    public CashTransfer(Guid id,Guid id2,decimal d)
    {
        this.accountId = id;
        this.receiverId = id2;
        this.Amount = d;
    }


}



