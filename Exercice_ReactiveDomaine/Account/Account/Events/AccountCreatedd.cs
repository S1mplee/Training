using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class AccountCreated : Message
    {
        public readonly Guid Id;
        public string Holdername;
        public decimal cash;
        public decimal overdraft;
        public decimal wiretranferlimit;
        public bool blocked;
        public AccountCreated(Guid id,string name,decimal d,decimal d2,decimal d3)
        {
            Id = id;
            Holdername = name;
            cash = d;
            overdraft = d2;
            wiretranferlimit = d3;
            blocked = false;
        }
    }
}
