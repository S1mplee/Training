using Account;
using System;

namespace TestAccountBalance
{
    public class AccountUnblocked : Event
    {
        public Guid id;

        public AccountUnblocked(Guid id)
        {
            this.id = id;
        }
    }
}