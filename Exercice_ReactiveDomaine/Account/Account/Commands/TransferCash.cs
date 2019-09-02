using ReactiveDomain.Messaging;
using System;

namespace Account
{
    public class TransferCash : Command
    {
        public Guid id;
        public decimal amount;
        public DateTime TransferDate;
        public TransferCash(Guid id, decimal amount,DateTime date) 
        {
            this.id = id;
            this.amount = amount;
            this.TransferDate = date;
        }
    }
}