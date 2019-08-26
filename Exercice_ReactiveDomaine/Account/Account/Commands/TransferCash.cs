using ReactiveDomain.Messaging;
using System;

namespace Account
{
    public class TransferCash : Command
    {
        public Guid id;
        public Guid reciverId;
        public decimal amount;

        public TransferCash(Guid id, Guid reciverId, decimal amount)
        {
            this.id = id;
            this.reciverId = reciverId;
            this.amount = amount;
        }
    }
}