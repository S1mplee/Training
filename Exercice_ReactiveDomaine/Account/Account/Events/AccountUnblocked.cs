using Account;
using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class AccountUnblocked : Message
    {
        public Guid id;

        public AccountUnblocked(Guid id)
        {
            this.id = id;
        }
    }
}