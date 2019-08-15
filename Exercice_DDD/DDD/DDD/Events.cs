using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Event
{

}

public class AccountCreated : Event
{
    public Guid Id;
    public string _holderName;
    public decimal _overdraftLimit;
    public decimal _wireTransertLimit;
    public bool _blocked;
    public decimal _cash;
    public AccountCreated(Guid id,string name,decimal d,decimal d2,decimal d3,bool b)
    {
        this.Id = id;
        this._holderName = name;
        this._overdraftLimit = d;
        this._wireTransertLimit = d2;
        this._blocked = b;
        this._cash = d3;
    }

    public override string ToString()
    {
        return "" + Id + " " + _holderName + " " + _overdraftLimit + " " + _wireTransertLimit + " " + _blocked + " " + _cash;
    }
}
public class CashDeposed : Event
{
    public readonly Guid AccountId ;
    public readonly decimal Amount;

    public CashDeposed(Guid id,decimal a)
    {
        this.AccountId = id;
        this.Amount = a;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

public class ChequeDeposed : Event
{
    public readonly Guid AccountId;
    public readonly decimal Amount;

    public ChequeDeposed(Guid id, decimal a)
    {
        this.AccountId = id;
        this.Amount = a;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

public class CashWithdrawn : Event
{
    public readonly Guid AccountId;
    public readonly decimal Amount;

    public CashWithdrawn(Guid id, decimal a)
    {
        this.AccountId = id;
        this.Amount = a;
    }

    public override string ToString()
    {
        return "" + AccountId + " " + Amount;
    }
}

public class AccountBlocked : Event
{
    public readonly Guid AccountId;
    public AccountBlocked(Guid id)
    {
        this.AccountId = id;
    }

    public override string ToString()
    {
        return "" + AccountId;
    }
}

public class AccountUnBlocked : Event
{
    public readonly Guid AccountId;
    public AccountUnBlocked(Guid id)
    {
        this.AccountId = id;
    }
    public override string ToString()
    {
        return "" + AccountId;
    }
}

public class CashTransfered : Event
{
    public readonly Guid AccountId;
    public readonly Guid receiverId;
    public readonly decimal Amount;

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

