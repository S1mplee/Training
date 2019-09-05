using EventStore.ClientAPI;
using ReactiveDomain;
using ReactiveDomain.EventStore;
using ReactiveDomain.Foundation;
using System;

namespace TESTDICT
{
    public class Service
    {
        private IStreamStoreConnection conn;
        public IRepository repo;
        private Guid _accountId = Guid.NewGuid();
        public ReadModel _readModel;
        public CommandHandler cmd;
        public void Bootstrap()
        {
            IEventStoreConnection esConnection = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@localhost:1113");
            conn = new EventStoreConnectionWrapper(esConnection);
            esConnection.Connected += (_, __) => Console.WriteLine("Connected");
            esConnection.ConnectAsync().Wait();
            IStreamNameBuilder namer = new PrefixedCamelCaseStreamNameBuilder();
            IEventSerializer ser = new JsonMessageSerializer();
            repo = new StreamStoreRepository(namer, conn, ser);
            // var acc = repo.GetById<AccountBalance>(Guid.Parse("e346c867-a9bb-4337-a91c-b8d51773897b"));
            IListener listener = new StreamListener("order", conn, namer, ser);
            _readModel = new ReadModel(() => listener);
            cmd = new CommandHandler(repo);
            //   var cmd = new TransferCash(Guid.Parse("9970dc8c-b22a-4f93-87be-fd2e798beea2"),Guid.Parse("bb554f9a-7a38-4c74-84c3-b50abe5bb1d4"),1000);
            //  var g = Guid.NewGuid();
             // var cmd = new CreateOrder(g, "Buy", "Asset2", 200, 5);
            //    var cmd = new CreateAccount(g, "Ahmed");
            //  var cmd2 = new SetDailyTransfertLimit(g, 1000);
             //   Console.WriteLine(Command.Handle(cmd));
            //   Thread.Sleep(1000);
            //   Console.WriteLine(Command.Handle(cmd2));
            //     Thread.Sleep(1000);
            //  _readModel.show();
            /*
            var acc = repo.GetById<AccountBalance>(Guid.Parse("96e90f4c-04e9-4ec3-b7f3-8627bd62b6fc"));
            try
            {
                acc.WireTransfer(Guid.Parse("49ad25ad-d1c8-4742-8429-82093536e71d"),263);
                repo.Save(acc);
            }
            catch (Exception ex)
            {
                repo.Save(acc);
            }
            */
        }
    }
}

