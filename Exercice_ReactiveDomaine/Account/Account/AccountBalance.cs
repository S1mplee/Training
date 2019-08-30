
using ReactiveDomain;
using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<Cheque> _list;

        public AccountBalance(Guid id,string name) : this()
        {
            if (id == null || string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Inputs !");
            Raise(new AccountCreated(id,name));
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
                var ch = new Cheque(evt.amount, evt.Date);
                if (CheckDate(evt.Date)) { this._cash += ch.Amount;
                    ch.Checked = true;
                }

                this._list.Add(ch);
            });
            Register<CashDeposed>(evt => this._cash += evt.amount);
            Register<AccountUnblocked>(evt => _blocked = false);
            Register<AccountBlocked>(evt => this._blocked = true);
            Register<CashWithdrawn>(evt => this._cash -= evt.amount);
            Register<CashTransfered>(evt =>
            {
                this._cash -= evt.amount;
                this._dailyTransfertAmount += evt.amount;
            });
            Register<OverDraftlimitSet>(evt => this._overdraft = evt.amount);
            Register<DailyWireTransfertLimitSet>(evt => this._wiretranferlimit = evt.amount);
        
        }

       
        public void DeposeCheque(decimal amount,DateTime date)
        {
            if (amount <= 0) throw new ArgumentException("Invalid amount");
            Raise(new ChequeDeposed(this.Id, amount,date));

            if (_blocked == true && ((this._cash < 0 && (Math.Abs(this._cash)) < this._overdraft) || this._cash >= 0))
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


            if (_blocked == true && ((this._cash < 0 && (Math.Abs(this._cash)) < this._overdraft) || this._cash >= 0))
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

        public void WireTransfer(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");

            if (amount + this._dailyTransfertAmount > this._wiretranferlimit)
            {
                Raise(new AccountBlocked(this.Id));
                throw new ArgumentException("Daily Wire Transfert Limit Surpassed !");
            }

            Raise(new CashTransfered(this.Id, amount));

            
        }

        private bool CheckDate(DateTime date)
        {
            if (date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday
            && date.TimeOfDay >= TimeSpan.Parse("09:00:00")
            && date.TimeOfDay <= TimeSpan.Parse("17:00:00")) return true;
            return false;
        }

    }
}
