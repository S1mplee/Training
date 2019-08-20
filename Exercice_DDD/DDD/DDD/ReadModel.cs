using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD
{
    public class ReadModel
    {

        public Account account;

        public void Handle(AccountCreated evt)
        {
            account = new Account(evt.Id, evt._holderName, evt._overdraftLimit, evt._wireTransertLimit,evt._cash);

        }

        public void Handle(AccountBlocked evt)
        {
            if (account.Blocked == true) throw new InvalidOperationException("Already Blocked !");
            account.Blocked = true;
        }

        public void Handle(AccountUnBlocked evt)
        {
            if (account.Blocked == false) throw new InvalidOperationException("Already UnBlocked !");
            account.Blocked = false;
        }



        public void Handle(CashDeposed evt)
        {
            if (evt.Amount < 0) throw new InvalidOperationException("Negative Amount !");
            account.Cash += evt.Amount;
        }

        public void Handle(CashTransfered evt)
        {
            if (evt.Amount < 0) throw new InvalidOperationException("Negative Amount !");
            account.Cash += evt.Amount;
        }

        public void Handle(CashWithdrawn evt)
        {
            if (evt.Amount < 0) throw new InvalidOperationException("Negative Amount !");
            account.Cash -= evt.Amount;
        }

        public void Handle(ChequeDeposed evt)
        {
            if (evt.Amount < 0) throw new InvalidOperationException("Negative Amount !");
            account.Cash += evt.Amount;
        }







    }
    public class Account
    {

        public Guid Id;
        public string HolderName;
        public decimal Cash;
        public decimal OverdraftLimit;
        public decimal WireTransertLimit;
        public bool Blocked;

        public Account(Guid id, string n, decimal d1, decimal d2, decimal d3)
        {
            Id = id;
            HolderName = n;
            Cash = d3;
            OverdraftLimit = d1;
            WireTransertLimit = d2;
            Blocked = false;
        }

    }
}
