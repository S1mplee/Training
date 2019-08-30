using Account;
using System;

namespace TestAccountBalance
{
    public class AccountBlocked : Event
    {
        public Guid id;

        public AccountBlocked(Guid id)
        {
            this.id = id;
        }
    }
}