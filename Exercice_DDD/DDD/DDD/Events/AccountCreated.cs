using System;

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

