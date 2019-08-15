using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Event
{

}

public class CashDeposed : Event
{
    public readonly Guid AccountId;
    public readonly decimal Amount;

    public CashDeposed(Guid id,decimal a)
    {
        this.AccountId = id;
        this.Amount = a;
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
}

public class AccountBlocked : Event
{
    public readonly Guid AccountId;
    public AccountBlocked(Guid id)
    {
        this.AccountId = id;
    }
}

public class AccountUnBlocked : Event
{
    public readonly Guid AccountId;
    public AccountUnBlocked(Guid id)
    {
        this.AccountId = id;
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
}

