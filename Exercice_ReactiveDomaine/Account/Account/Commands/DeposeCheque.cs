using ReactiveDomain.Messaging;
using System;

namespace Account
{
    public class DeposeCheque : Command
    {
        public readonly Guid Id;
        public decimal amount;
        public DeposeCheque(Guid id, decimal amount)
        {
            this.Id = id;
            this.amount = amount;
        }
    }
}