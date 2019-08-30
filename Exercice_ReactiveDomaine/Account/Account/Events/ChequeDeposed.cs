using System;
using Account;

namespace TestAccountBalance
{

    public class ChequeDeposed : Event
    {
        public readonly Guid Id;
        public decimal amount;
        public readonly DateTime Date;
        public ChequeDeposed(Guid id,decimal amount,DateTime date)
        {
            this.Id = id;
            this.amount = amount;
            this.Date = date;
        }
    }
}
