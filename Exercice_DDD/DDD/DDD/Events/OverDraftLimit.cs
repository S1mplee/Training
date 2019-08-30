using System;

    public class OverDraftlimitSet : Event
    {
        public readonly Guid AccountId;
        public readonly decimal Amount;

        public OverDraftlimitSet(Guid id, decimal amount)
        {
            this.AccountId = id;
            this.Amount = amount;
        }
    }

