using Account;
using EventStore.ClientAPI;
using ReactiveDomain;
using ReactiveDomain.EventStore;
using ReactiveDomain.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class TestService
    {
        public IStreamStoreConnection conn;
        public IRepository repo;
        public Guid _accountId = Guid.NewGuid();
        public AccountBalanceReadModel _readModel;
        public AccountCommandHandler Command;
        public TestService()
        {
            IEventStoreConnection esConnection = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@localhost:1113");
            conn = new EventStoreConnectionWrapper(esConnection);
            esConnection.Connected += (_, __) => Console.WriteLine("Connected");
            esConnection.ConnectAsync().Wait();
            IStreamNameBuilder namer = new PrefixedCamelCaseStreamNameBuilder();
            IEventSerializer ser = new JsonMessageSerializer();
            repo = new StreamStoreRepository(namer, conn, ser);
            // var acc = repo.GetById<AccountBalance>(Guid.Parse("e346c867-a9bb-4337-a91c-b8d51773897b"));
            IListener listener = new StreamListener("Account", conn, namer, ser);
            _readModel = new AccountBalanceReadModel(() => listener);
             Command = new AccountCommandHandler(repo);
        }
    }
}
