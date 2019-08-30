using ReactiveDomain.Messaging;
using System;

namespace Account.Commands
{

    public class SetOverdraftLimit : Command
    {
        public Guid id;
        public decimal amount;

        public SetOverdraftLimit(Guid id, decimal amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
}
