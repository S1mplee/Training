using ReactiveDomain.Messaging;
using System;

namespace Account
{
    public class WithDrawCash : Command
    {
        public Guid id;
        public decimal amount;

        public WithDrawCash(Guid id, decimal amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
}