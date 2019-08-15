using System;

namespace DDD.DomainModel
{
    public class AccountBalance : AggregateRoot
    {
        private Guid Id;
        private string _holderName;
        private decimal _cash;
        private decimal _overdraftLimit;
        private decimal _wireTransertLimit;
        private bool _blocked;

        public AccountBalance() : base()
        {

        }

        public void Create(Guid id,string name,decimal overdraft,decimal wiretransfert,decimal cash)
        {
            if (id == null || string.IsNullOrWhiteSpace(name) || overdraft < 0 || wiretransfert < 0 || cash < 0) throw new ArgumentException("Invalid Input !");

            this.Id = id;
            this._holderName = name;
            this._overdraftLimit = overdraft;
            this._wireTransertLimit = wiretransfert;
            this._cash = cash;
            this._blocked = false;

            this.events.Add(new AccountCreated(id, name, overdraft, wiretransfert, cash,false));

        }

        public void DeposeCheque(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
            this._cash += amount;
            this.events.Add(new ChequeDeposed(this.Id, amount));

            
            if (_blocked == true && ((this._cash < 0 && (Math.Abs(this._cash)) < this._overdraftLimit) || this._cash >= 0))
            {
                this._blocked = false;
                this.events.Add(new AccountUnBlocked(this.Id));
            }
        }

        public void DeposeCash(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
            this._cash += amount;
            this.events.Add(new CashDeposed(this.Id, amount));


            if (_blocked == true && ((this._cash < 0 && (Math.Abs(this._cash)) < this._overdraftLimit) || this._cash >= 0))
            {
                this._blocked = false;
                this.events.Add(new AccountUnBlocked(this.Id));
            }
        }

        public void WithdrawCash(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
            if ((this._cash - amount) < 0  && Math.Abs(this._cash - amount) > this._overdraftLimit)
            {
                this._blocked = true;
                this.events.Add(new AccountBlocked(this.Id));
                throw new ArgumentException("OverDraft limit !");
            }

            this._cash = this._cash - amount;
            this.events.Add(new CashWithdrawn(this.Id, amount));
        }

        public void WireTransfer(Guid reciverId,decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");

            this._cash = this._cash - amount;
            this.events.Add(new CashTransfered(this.Id, reciverId, amount));

            if (amount > this._wireTransertLimit)
            {
                this._blocked = true;
                this.events.Add(new AccountBlocked(this.Id));
            }
        }



    }
}
