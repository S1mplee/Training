
using System;
using System.Collections.Generic;

namespace DDD.DomainModel
{
    public class AccountBalance : AggregateRoot
    {
        private string _holderName;
        private decimal _balance;
        private decimal _overdraftLimit;
        private decimal _wireTransertLimit;
        private decimal _DailywireTransfertAchieved;
        private bool _blocked;
        private List<Cheque> _cheques = new List<Cheque>();

        public AccountBalance() : base()
        {

        }

        public void Create(Guid id,string name)
        {
             if (id == null || string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Input !");
            var evt = new AccountCreated(id, name);
            SetState(evt);
            this.events.Add(evt);

        }

        public void SetOverDraftLimit(Guid id,decimal amount)
        {
            if (id == null || amount < 0) throw new ArgumentException("Invalid Inputs");
            var evt = new OverDraftlimitSet(id, amount);
            SetState(evt);
            this.events.Add(evt);
        }

        public void SetWireTransfertLimit(Guid id,decimal amount)
        {
            if (id == null || amount < 0) throw new ArgumentException("Invalid Inputs");
            var evt = new DailyWireTransfertLimitSet(id, amount);
            SetState(evt);
            this.events.Add(evt);
        }

       
        
        public void DeposeCheque(Cheque cheque)
        {
            if (cheque.Amount <= 0) throw new ArgumentException("invalid Amount"); 

            var evt = new ChequeDeposed(this.Id, cheque.Amount,cheque.Date);
                SetState(evt);
                this.events.Add(evt);
            
            if (_blocked == true && ((this._balance < 0 && (Math.Abs(this._balance)) < this._overdraftLimit) || this._balance >= 0))
            {
                var evt2 = new AccountUnBlocked(this.Id);
                SetState(evt2);
                this.events.Add(evt2);
            }
        }
        
       
        public void DeposeCash(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
            var evt = new CashDeposed(this.Id, amount);
            SetState(evt);
            this.events.Add(evt);


            if (_blocked == true && ((this._balance < 0 && (Math.Abs(this._balance)) < this._overdraftLimit) || this._balance >= 0))
            {
                var evt2 = new AccountUnBlocked(this.Id);
                SetState(evt2);
                this.events.Add(evt2);
            }
        }


        public void WithdrawCash(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
            var Total = this._balance + this._overdraftLimit;
            if (Total - amount < 0)
            {
                var evt = new AccountBlocked(this.Id);
                SetState(evt);
                this.events.Add(new AccountBlocked(this.Id));
                throw new ArgumentException("OverDraft limit !");
            }

            var evt2 = new CashWithdrawn(this.Id, amount);
            SetState(evt2);
            this.events.Add(evt2);
        }



        public void WireTransfer(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
         
            if (this._DailywireTransfertAchieved +  amount > this._wireTransertLimit)
            {
                var evt2 = new AccountBlocked(this.Id);
                SetState(evt2);
                this.events.Add(evt2);
            } else
            {
                var evt = new CashTransfered(this.Id, amount);
                SetState(evt);
                this.events.Add(evt);
            }

          

        }
        public void SetState(ChequeDeposed evt)
        {
            var cheque = new Cheque(evt.Amount, evt.Date);
            if (VerifDate(evt.Date))
            {
                this._balance += cheque.Amount;
                cheque.Checked = true;
            }
            this._cheques.Add(cheque);
        }

        public void SetState(CashTransfered evt)
        {
            this._balance -= evt.Amount;
            this._DailywireTransfertAchieved += evt.Amount;
        }

        public void SetState(AccountCreated evt)
        {
            this.Id = evt.AccountId;
            this._holderName = evt.HolderName;
            this._blocked = false;
        }

        public void SetState(CashDeposed evt)
        {
            this._balance += evt.Amount;
        }

        public void SetState(CashWithdrawn evt)
        {
            this._balance -= evt.Amount;
        }

        public void SetState(AccountBlocked evt)
        {
            this._blocked = true;
        }

        public void SetState(AccountUnBlocked evt)
        {
            this._blocked = false;
        }

        public void SetState(OverDraftlimitSet evt)
        {
            this._overdraftLimit = evt.Amount;
        }

        public void SetState(DailyWireTransfertLimitSet evt)
        {
            this._wireTransertLimit = evt.Amount;
        }

        private bool VerifDate(DateTime date)
        {
            if (date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday
             && date.TimeOfDay >= TimeSpan.Parse("09:00:00")
             && date.TimeOfDay <= TimeSpan.Parse("17:00:00")) return true; 
            return false;
        }


    }
}
