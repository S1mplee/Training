using Account;
using System;

namespace TestAccountBalance
{
    public class CashDeposed : Event 
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