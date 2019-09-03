using Account;
using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class CashWithdrawn : Message
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