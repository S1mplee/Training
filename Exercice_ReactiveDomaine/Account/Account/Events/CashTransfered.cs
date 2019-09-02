using Account;
using System;

namespace TestAccountBalance
{
    public class CashTransfered : Event
    {
        public Guid id;
        public decimal amount;
        public readonly DateTime TransferDate;
        public CashTransfered(Guid id, decimal amount,DateTime date)
        {
            this.id = id;
            this.amount = amount;
            this.TransferDate = date;
        }
    }
}