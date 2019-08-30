using Account;
using System;

namespace TestAccountBalance
{
    public class AccountCreated : Event
    {
        public readonly Guid Id;
        public string Holdername;
        public AccountCreated(Guid id,string name)
        {
            Id = id;
            Holdername = name;
        }
    }
}
