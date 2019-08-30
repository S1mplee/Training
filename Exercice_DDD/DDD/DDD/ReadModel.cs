using System;

namespace DDD
{
    public class ReadModel : IEventHandler<AccountCreated> , IEventHandler<AccountBlocked> , IEventHandler<AccountUnBlocked>,
        IEventHandler<CashDeposed> , IEventHandler<CashTransfered> , IEventHandler<CashWithdrawn> , IEventHandler<ChequeDeposed> 
    {

        public Account account;

        public void Handle(AccountCreated evt)
        {
           account = new Account(evt.AccountId, evt.HolderName);

        }

        public void Handle(AccountBlocked evt)
        {
            account.Blocked = true;
        }

        public void Handle(AccountUnBlocked evt)
        {
            account.Blocked = false;
        }



        public void Handle(CashDeposed evt)
        {
            account.Cash += evt.Amount;
        }

        public void Handle(CashTransfered evt)
        {
            account.Cash += evt.Amount;
        }

        public void Handle(CashWithdrawn evt)
        {
            account.Cash -= evt.Amount;
        }

        public void Handle(ChequeDeposed evt)
        {
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

        public Account(Guid id, string name)
        {
            Id = id;
            HolderName = name;
            Blocked = false;
        }

    }
}
