using System;

public class AccountBlocked : Event
{
    public  Guid AccountId;
    public AccountBlocked(Guid id)
    {
        this.AccountId = id;
    }

    public override string ToString()
    {
        return "" + AccountId;
    }
}

