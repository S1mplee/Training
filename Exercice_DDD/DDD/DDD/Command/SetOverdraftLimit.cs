using Commands;
using System;


    public class SetOverdraftLimit : Command
    {
        public readonly Guid AccountId;
        public readonly decimal Amount;

        public SetOverdraftLimit(Guid id,decimal amount)
        {
            this.AccountId = id;
            this.Amount = amount;
        }
    }

