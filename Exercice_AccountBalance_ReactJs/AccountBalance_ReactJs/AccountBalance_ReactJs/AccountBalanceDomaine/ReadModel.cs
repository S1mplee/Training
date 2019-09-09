using ReactiveDomain.Foundation;
using ReactiveDomain.Messaging.Bus;
using System;
using System.Collections.Generic;
using TestAccountBalance;

namespace Account
{

    public class AccountBalanceReadModel :
        ReadModelBase,
        IHandle<AccountCreated>,
        IHandle<ChequeDeposed>,
        IHandle<AccountUnblocked>,
        IHandle<AccountBlocked>,
        IHandle<CashDeposed>,
        IHandle<CashWithdrawn>,
        IHandle<CashTransfered>,
        IHandle<OverDraftlimitSet>,
        IHandle<DailyWireTransfertLimitSet>
    {
        public List<account> list = new List<account>();
        public AccountBalanceReadModel(Func<IListener> listener) : base("", listener)
        {
            EventStream.Subscribe<AccountCreated>(this);
            EventStream.Subscribe<ChequeDeposed>(this);
            EventStream.Subscribe<AccountBlocked>(this);
            EventStream.Subscribe<AccountUnblocked>(this);
            EventStream.Subscribe<CashDeposed>(this);
            EventStream.Subscribe<CashWithdrawn>(this);
            EventStream.Subscribe<CashTransfered>(this);
            EventStream.Subscribe<OverDraftlimitSet>(this);
            EventStream.Subscribe<DailyWireTransfertLimitSet>(this);
            Start<AccountBalance>(null, true);
        }

        public void Handle(AccountCreated message)
        {
            list.Add(new account
            {
                Id = message.Id,
                HolderName = message.Holdername,
                blocked = false
            });
        }

        public void Handle(ChequeDeposed message)
        {
            if (DateTime.Now >= message.ReleaseDate)
            {
                list.Find(x => x.Id == message.Id).cash += message.amount;
            }
        }

        public void Handle(AccountUnblocked message)
        {
            list.Find(x => x.Id == message.id).blocked = false;

        }

        public void Handle(AccountBlocked message)
        {
            list.Find(x => x.Id == message.id).blocked = true;

        }

        public void Handle(CashDeposed message)
        {
            list.Find(x => x.Id == message.id).cash += message.amount;

        }

        public void Handle(CashWithdrawn message)
        {
            list.Find(x => x.Id == message.id).cash -= message.amount;
        }

        public void Handle(CashTransfered message)
        {
           
            list.Find(x => x.Id == message.id).cash -= message.amount;
            list.Find(x => x.Id == message.id).DailyWireTransfetAchieved += message.amount;

            if ((DateTime.Now - message.TransferDate).TotalHours > 24)
                list.Find(x => x.Id == message.id).DailyWireTransfetAchieved = 0;
        }

        public void Handle(OverDraftlimitSet message)
        {
            list.Find(x => x.Id == message.id).overdraftlimit = message.amount;
        }

        public void Handle(DailyWireTransfertLimitSet message)
        {
            list.Find(x => x.Id == message.id).wiretransfertlimit = message.amount;
        }

        public void show()
        {
            foreach (var elem in list)
            {
                Console.WriteLine(elem.HolderName + " " + elem.cash + " " + elem.blocked + " " + elem.overdraftlimit + " " + elem.wiretransfertlimit);
            }
        }


    }

}