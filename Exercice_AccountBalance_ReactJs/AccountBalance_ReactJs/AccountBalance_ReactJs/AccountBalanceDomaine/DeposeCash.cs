using ReactiveDomain.Messaging;
using System;

namespace Account
{
    public class DeposeCash : Command
    {
        public Guid id;
        public decimal amount;

        public DeposeCash(Guid id, decimal amount) 
        {
            this.id = id;
            this.amount = amount;
        }
    }
}