using Commands;
using System;

public class CreateAccount : Command
{
    public readonly Guid AccountId;
    public readonly string HolderName;
    public CreateAccount(Guid id, string name)
    {
        this.AccountId = id;
        this.HolderName = name;
      
    }
}



