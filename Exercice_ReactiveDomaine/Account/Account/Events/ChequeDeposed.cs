using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAccountBalance
{

    public class ChequeDeposed : Message
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
