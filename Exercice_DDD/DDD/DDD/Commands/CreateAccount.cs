using System;

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



