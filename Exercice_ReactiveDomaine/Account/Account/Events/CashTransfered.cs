using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class CashTransfered : Message
    {
        public Guid id;
        public Guid reciverId;
        public decimal amount;

        public CashTransfered(Guid id, Guid reciverId, decimal amount)
        {
            this.id = id;
            this.reciverId = reciverId;
            this.amount = amount;
        }
    }
}