using System;

    public class DailyWireTransfertLimitSet : Event
    {
        public readonly Guid AccountId;
        public readonly decimal Amount;

        public DailyWireTransfertLimitSet(Guid id, decimal amount)
        {
            this.AccountId = id;
            this.Amount = amount;
        }
    }


