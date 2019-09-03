using Account;
using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class CashDeposed : Message
    {
        public Guid id;
        public decimal amount;

        public CashDeposed(Guid id, decimal amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
}