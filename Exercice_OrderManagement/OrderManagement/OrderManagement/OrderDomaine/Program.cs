using EventStore.ClientAPI;
using ReactiveDomain;
using ReactiveDomain.EventStore;
using ReactiveDomain.Foundation;
using ReactiveDomain.Messaging.Bus;
using System;
using System.Configuration;

namespace TESTDICT
{
    public class Service
    {
        private IStreamStoreConnection conn;
        public IRepository repo;
        private Guid _accountId = Guid.NewGuid();
        public ReadModel _readModel;
        public CommandHandler cmd;
        public Dispatcher Bus;

        public void Bootstrap()
        {
            string port = ConfigurationManager.AppSettings["ES_PORT"];
            string ip = ConfigurationManager.AppSettings["ES_IP"];
            string pwd = ConfigurationManager.AppSettings["ES_PWD"];
            string username = ConfigurationManager.AppSettings["ES_USERNAME"];


            IEventStoreConnection esConnection = EventStoreConnection.Create("ConnectTo=tcp://" + username + ":" + pwd + "@" + ip + ":" + port);
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
            Bus = new Dispatcher("MyDispatcher");
            Bus.Subscribe<CreateOrder>(cmd);

    }
}
}

