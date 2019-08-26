
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
        private bool _blocked;

        public AccountBalance(Guid id,string name,decimal cash,decimal over,decimal wire) : this()
        {
            if (id == null || string.IsNullOrWhiteSpace(name) || cash < 0 || over < 0 || wire < 0) throw new ArgumentException("Invalid Inputs !");
            Raise(new AccountCreated(id,name,cash,over,wire));
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
                _cash = evt.cash;
                _overdraft = evt.overdraft;
                _wiretranferlimit = evt.wiretranferlimit;
                _blocked = false;
            });
            
            Register<ChequeDeposed>(evt =>
            {
                this._cash = this._cash + evt.amount;
            });
            Register<CashDeposed>(evt => this._cash += evt.amount);
            Register<AccountUnblocked>(evt => _blocked = false);
            Register<AccountBlocked>(evt => this._blocked = true);
            Register<CashWithdrawn>(evt => this._cash -= evt.amount);
            Register<CashTransfered>(evt => this._cash -= evt.amount);

        
        }

        public void DeposeCheque(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Invalid amou");
            Raise(new ChequeDeposed(this.Id, amount));

            if (_blocked == true && ((this._cash < 0 && (Math.Abs(this._cash)) < this._overdraft) || this._cash >= 0))
            {
                Raise(new AccountUnblocked(Id));
            }
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

        public void WireTransfer(Guid reciverId, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("invalid Amount");

            this._cash = this._cash - amount;
            Raise(new CashTransfered(this.Id, reciverId, amount));

            if (amount > this._wiretranferlimit)
            {
                Raise(new AccountBlocked(this.Id));
            }
        }
    }
}
