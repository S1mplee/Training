using ReactiveDomain;
using ReactiveDomain.Messaging;
using System;

namespace TestAccountBalance
{
    public class AccountBalance : EventDrivenStateMachine
    {
        private string _Holdername;
        private decimal _cash;
        private decimal _overdraft;
        private decimal _wiretranferlimit;
        private decimal _dailyTransfertAmount;
        private bool _blocked;

        public AccountBalance(Guid id, string name) : this()
        {
            if (id == null || string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Inputs !");
            Raise(new AccountCreated(id, name));
        }
        class MySecretEvent : Message { }
        public AccountBalance()
        {
            setup();
        }

        public void setup()
        {
            Register<AccountCreated>(evt =>
            {
                Id = evt.Id;
                _Holdername = evt.Holdername;
                _blocked = false;
            });

            Register<ChequeDeposed>(evt =>
            {
                if (DateTime.Now >= evt.ReleaseDate)
                {
                    this._cash += evt.amount;
                }

            });
            Register<CashDeposed>(evt => this._cash += evt.amount);
            Register<AccountUnblocked>(evt => _blocked = false);
            Register<AccountBlocked>(evt => this._blocked = true);
            Register<CashWithdrawn>(evt => this._cash -= evt.amount);
            Register<CashTransfered>(evt =>
            {
              
                this._cash -= evt.amount;
                this._dailyTransfertAmount = this._dailyTransfertAmount + evt.amount;

                if ((DateTime.Now - evt.TransferDate).TotalHours > 24)
                { this._dailyTransfertAmount = 0; }

            });
            Register<OverDraftlimitSet>(evt => this._overdraft = evt.amount);
            Register<DailyWireTransfertLimitSet>(evt => this._wiretranferlimit = evt.amount);

        }


        public void DeposeCheque(decimal amount, DateTime DepositDate)
        {
            if (amount <= 0) throw new ArgumentException("Invalid amount");

            var ReleaseDate = GetReleaseDate(DepositDate);

            Raise(new ChequeDeposed(this.Id, amount, ReleaseDate));

            if (_blocked == true)
            {
                Raise(new AccountUnblocked(Id));
            }
        }

        public void SetOverDraftLimit(decimal amount)
        {
            if (amount < 0) throw new ArgumentException("Invalid Inputs");
            Raise(new OverDraftlimitSet(this.Id, amount));
        }

        public void SetWireTransfertLimit(decimal amount)
        {
            if (amount < 0) throw new ArgumentException("Invalid Inputs");
            Raise(new DailyWireTransfertLimitSet(this.Id, amount));
        }

        public void DeposeCash(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");
            Raise(new CashDeposed(this.Id, amount));


            if (_blocked == true)
            {
                Raise(new AccountUnblocked(this.Id));
            }
        }

        public void WithdrawCash(decimal amount)
        {

            if (amount <= 0) throw new ArgumentException("invalid Amount");
            if ((this._cash - amount) < 0 && Math.Abs(this._cash - amount) > this._overdraft)
            {
                Raise(new AccountBlocked(this.Id));
                throw new ArgumentException("OverDraft limit !");
            }

            Raise(new CashWithdrawn(this.Id, amount));

        }

        public void WireTransfer(decimal amount, DateTime TransferDate)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");

            if (amount + this._dailyTransfertAmount > this._wiretranferlimit)
            {
                Raise(new AccountBlocked(this.Id));
                throw new ArgumentException("Daily Wire Transfert Limit Surpassed !");
            }

            Raise(new CashTransfered(this.Id, amount, TransferDate));


        }

        private bool CheckDate(DateTime date)
        {
            if (date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday
            && date.TimeOfDay >= TimeSpan.Parse("09:00:00")
            && date.TimeOfDay <= TimeSpan.Parse("17:00:00")) return true;
            return false;
        }

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