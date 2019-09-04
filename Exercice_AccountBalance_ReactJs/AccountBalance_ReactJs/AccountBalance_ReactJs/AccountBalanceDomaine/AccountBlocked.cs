using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class AccountBlocked : Message
    {
        public Guid id;

        public AccountBlocked(Guid id)
        {
            this.id = id;
        }
    }
}