using System;
using System.Net;
using DDD.DomainModel;
using EventStore.ClientAPI;

namespace DDD.DomainService
{
    public class Repository : IRepository 
    {
        private IEventStoreConnection _conn;
        private BusMessage _bus;
        private string _address;
        private int _port;

        public Repository()
        {
            _address = "127.0.0.1";
            _port = 1113;
            _conn = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse(_address), _port));
            _conn.ConnectAsync().Wait();
            _bus = new BusMessage();
        }
        public AggregateRoot GetbyID(Guid id)
        {
            var readEvents = _conn.ReadStreamEventsForwardAsync("AccountBalance"+id.ToString(), 0, 100, true).Result;
            if (readEvents.Events.Length == 0) return null;

            var acc = new AccountBalance();
            foreach(var evt in readEvents.Events)
            {
                if (evt.Event.EventType.Equals("AccountCreated"))
                {
                    var res = evt.Event.ParseJson<AccountCreated>();
                    
                        acc.SetState(res);
                }
                if (evt.Event.EventType.Equals("CashDeposed"))
                {
                    var res = evt.Event.ParseJson<CashDeposed>();

                    acc.SetState(res); 
                }
                if (evt.Event.EventType.Equals("ChequeDeposed"))
                {
                    var res = evt.Event.ParseJson<ChequeDeposed>();
                   
                        acc.SetState(res);
                }
                if (evt.Event.EventType.Equals("CashTransfered"))
                {
                    var res = evt.Event.ParseJson<CashTransfered>();
                    
                        acc.SetState(res);    
                }
                if (evt.Event.EventType.Equals("AccountBlocked"))
                {
                    var res = evt.Event.ParseJson<AccountBlocked>();
                   
                        acc.SetState(res);   
                }
                if (evt.Event.EventType.Equals("AccountUnBlocked"))
                {
                    var res = evt.Event.ParseJson<AccountUnBlocked>();
                   
                        acc.SetState(res);
                    
                }
                if (evt.Event.EventType.Equals("CashWithdrawn"))
                {
                    var res = evt.Event.ParseJson<CashWithdrawn>();
                   
                        acc.SetState(res);  
                }
            }
            return acc;
        }

        public void SaveEvents(AggregateRoot agg)
        {
            foreach(var evt in agg.events)
            {
                _conn.AppendToStreamAsync("AccountBalance"+agg.Id.ToString(), ExpectedVersion.Any, evt.AsJson()).Wait();
            }
        }
    }
}
