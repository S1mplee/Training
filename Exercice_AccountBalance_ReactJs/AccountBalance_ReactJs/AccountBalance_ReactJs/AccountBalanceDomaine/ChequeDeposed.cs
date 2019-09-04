using System;
using ReactiveDomain.Messaging;

namespace TestAccountBalance
{

    public class ChequeDeposed : Message
    {
        public readonly Guid Id;
        public decimal amount;
        public DateTime ReleaseDate;
        public ChequeDeposed(Guid id, decimal amount, DateTime date)
        {
            this.Id = id;
            this.amount = amount;
            this.ReleaseDate = date;
        }
    }
}