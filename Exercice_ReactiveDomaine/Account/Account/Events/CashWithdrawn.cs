using Account;
using System;

namespace TestAccountBalance
{
    public class CashWithdrawn : Event
    {
        public Guid id;
        public decimal amount;

        public CashWithdrawn(Guid id, decimal amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
}