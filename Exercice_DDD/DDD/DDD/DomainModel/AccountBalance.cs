
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

        public void SetOverDraftLimit(decimal amount)
        {
            if ( amount < 0) throw new ArgumentException("Invalid Inputs");
            var evt = new OverDraftlimitSet(this.Id, amount);
            SetState(evt);
            this.events.Add(evt);
        }

        public void SetWireTransfertLimit(decimal amount)
        {
            if ( amount < 0) throw new ArgumentException("Invalid Inputs");
            var evt = new DailyWireTransfertLimitSet(this.Id,amount);
            SetState(evt);
            this.events.Add(evt);
        }

       
        
        public void DeposeCheque(decimal amount,DateTime DepositDate)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");

            var ReleaseDate = GetReleaseDate(DepositDate);
            var OldAmount = this._balance;

            var evt = new ChequeDeposed(this.Id,amount,DepositDate, ReleaseDate);
                SetState(evt);
                this.events.Add(evt);
            
            if (_blocked == true && this._balance > OldAmount)
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


            if (_blocked == true )
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



        public void WireTransfer(decimal amount,DateTime TransferDate)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
         
            if (this._DailywireTransfertAchieved +  amount > this._wireTransertLimit)
            {
                var evt2 = new AccountBlocked(this.Id);
                SetState(evt2);
                this.events.Add(evt2);
            } else
            {
                var evt = new CashTransfered(this.Id, amount, TransferDate);
                SetState(evt);
                this.events.Add(evt);
            }

          

        }
        public void SetState(ChequeDeposed evt)
        {
            if (DateTime.Now >= evt.ClearDate)
            {
                this._balance += evt.Amount;
            }
        }

        public void SetState(CashTransfered evt)
        {
            if ((DateTime.Now - evt.TransferDate).TotalHours > 24)
            {
                this._DailywireTransfertAchieved = 0;
            }

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

        //Gets The Next Business Day
        public DateTime GetReleaseDate(DateTime date)
        {
            var date1 = DateTime.Parse(date.Date.ToString());
            var date_8h = date1.AddHours(8);
            var date_17h = date1.AddHours(17);

            if (date1.DayOfWeek == DayOfWeek.Sunday)
            {
                var d = date1.AddHours(32);
                return d;
            }
            else if (date1.DayOfWeek == DayOfWeek.Saturday)
            {
                var d = date1.AddHours(56);
                return d;
            }
            else if (date1.DayOfWeek == DayOfWeek.Friday && date.Hour >= 17)
            {
                var d = date1.AddHours(80);
                return d;
            }



            else if (date < date_8h)
            {
                var m = DateTime.Parse(date.Date.ToString());
                var c = m.AddHours(8);

                var businessDay = date + (c - date);
                return businessDay;
            }

            else if (date > date_17h)
            {
                var k = DateTime.Parse(date.Date.ToString());
                var mm = k.AddHours(32);

                var businessDay = date + (mm - date);
                return businessDay;
            }


            return date;
        }
    }
}


    

