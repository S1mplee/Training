using Commands;
using System;

    public class SetDailyTransfertLimit : Command
    {
        public readonly Guid AccountId;
        public readonly decimal Amount;

        public SetDailyTransfertLimit(Guid id,decimal amount)
        {
            this.AccountId = id;
            this.Amount = amount;
        }
    }

