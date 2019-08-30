using Account;
using System;

namespace TestAccountBalance
{
    public class CashTransfered : Event
    {
        public Guid id;
        public decimal amount;

        public CashTransfered(Guid id, decimal amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
}