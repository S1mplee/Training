using System;

public class AccountCreated : Event
{
    public Guid AccountId;
    public string HolderName;
    public AccountCreated(Guid id,string name)
    {
        this.AccountId = id;
        this.HolderName = name;
    }

    
}

