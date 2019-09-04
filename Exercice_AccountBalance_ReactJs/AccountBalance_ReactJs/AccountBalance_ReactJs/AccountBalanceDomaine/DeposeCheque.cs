using ReactiveDomain.Messaging;
using System;

namespace Account
{
    public class DeposeCheque : Command
    {
        public readonly Guid Id;
        public decimal amount;
        public DateTime DepositDate;
        public DeposeCheque(Guid id, decimal amount, DateTime Date)
        {
            this.Id = id;
            this.amount = amount;
            this.DepositDate = Date;
        }
    }
}