using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class AccountCreated : Message
    {
        public readonly Guid Id;
        public string Holdername;
        public AccountCreated(Guid id, string name)
        {
            Id = id;
            Holdername = name;
        }
    }
}