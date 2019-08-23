using System;

public class AccountUnBlocked : Event
{
    public Guid AccountId;
    public AccountUnBlocked(Guid id)
    {
        this.AccountId = id;
    }
    public override string ToString()
    {
        return "" + AccountId;
    }
}

