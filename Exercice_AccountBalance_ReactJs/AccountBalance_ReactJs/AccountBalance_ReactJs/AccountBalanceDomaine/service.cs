using Account;
using AccountBalance_ReactJs.AccountBalanceDomaine;
using EventStore.ClientAPI;
using ReactiveDomain.EventStore;
using ReactiveDomain.Foundation;
using ReactiveDomain.Messaging.Bus;
using System.Configuration;

namespace Reactjs_Account
{
    public class Service
    {
        public readonly AccountBalanceReadModel _readModel;
        private readonly IRepository _repo;
        private readonly AccountCommandHandler _cmdHandler;
        public Bus bus;
        public Service()
        {
            string port = ConfigurationManager.AppSettings["ES_PORT"];
            string ip = ConfigurationManager.AppSettings["ES_IP"];
            string pwd = ConfigurationManager.AppSettings["ES_PWD"];
            string username = ConfigurationManager.AppSettings["ES_USERNAME"];



            IEventStoreConnection esConnection = EventStoreConnection.Create("ConnectTo=tcp://"+username+":"+pwd+"@"+ip+":"+port);
            var conn = new EventStoreConnectionWrapper(esConnection);
            esConnection.Connected += (_, __) => { };
            //Console.WriteLine("Connected");
            esConnection.ConnectAsync().Wait();
            IStreamNameBuilder namer = new PrefixedCamelCaseStreamNameBuilder();
            IEventSerializer ser = new JsonMessageSerializer();
            _repo = new StreamStoreRepository(namer, conn, ser);
            _cmdHandler = new AccountCommandHandler(_repo);

            var listener = new StreamListener("Account", conn, namer,ser);
            _readModel = new AccountBalanceReadModel(() => listener);

            bus = new Bus();

            bus.RegisterHandler<CreateAccount>(_cmdHandler.Handle);
            bus.RegisterHandler<DeposeCash>(_cmdHandler.Handle);
            bus.RegisterHandler<DeposeCheque>(_cmdHandler.Handle);
            bus.RegisterHandler<SetDailyTransfertLimit>(_cmdHandler.Handle);
            bus.RegisterHandler<SetOverdraftLimit>(_cmdHandler.Handle);
            bus.RegisterHandler<WithDrawCash>(_cmdHandler.Handle);
            bus.RegisterHandler<TransferCash>(_cmdHandler.Handle);



        }
    }

}
