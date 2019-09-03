using Account;
using Newtonsoft.Json;
using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class CashTransfered : Message
    {
        public Guid id;
        public decimal amount;
        public DateTime TransferDate;

        public CashTransfered(Guid id, decimal amount,DateTime date)
        {
            this.id = id;
            this.amount = amount;
            this.TransferDate = date;
        }
    }
}