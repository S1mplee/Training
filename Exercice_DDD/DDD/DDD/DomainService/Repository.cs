using System;
using System.Net;
using DDD.DomainModel;
using EventStore.ClientAPI;

namespace DDD.DomainService
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot 
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
            bool verif = false;
            var readEvents = _conn.ReadStreamEventsForwardAsync("AccountBalance", 0, 100, true).Result;
            var acc = new AccountBalance();
            foreach(var evt in readEvents.Events)
            {
                if (evt.Event.EventType.Equals("AccountCreated"))
                {
                    var res = evt.Event.ParseJson<AccountCreated>();
                    if(res.AccountId == id )
                    {
                        acc.SetState(res);
                        verif = true;
                    }
                }
                if (evt.Event.EventType.Equals("CashDeposed"))
                {
                    var res = evt.Event.ParseJson<CashDeposed>();
                    if (res.AccountId == id)
                    {
                        acc.SetState(res);
                        verif = true;
                    }
                }
                if (evt.Event.EventType.Equals("ChequeDeposed"))
                {
                    var res = evt.Event.ParseJson<ChequeDeposed>();
                    if (res.AccountId == id)
                    {
                        acc.SetState(res);
                        verif = true;
                    }
                }
                if (evt.Event.EventType.Equals("CashTransfered"))
                {
                    var res = evt.Event.ParseJson<CashTransfered>();
                    if (res.AccountId == id)
                    {
                        acc.SetState(res);
                        verif = true;
                    }
                }
                if (evt.Event.EventType.Equals("AccountBlocked"))
                {
                    var res = evt.Event.ParseJson<AccountBlocked>();
                    if (res.AccountId == id)
                    {
                        acc.SetState(res);
                        verif = true;
                    }
                    // acc.acc(res.receiverId, res.Amount);
                }
                if (evt.Event.EventType.Equals("AccountUnBlocked"))
                {
                    var res = evt.Event.ParseJson<AccountUnBlocked>();
                    if (res.AccountId == id)
                    {
                        acc.SetState(res);
                        verif = true;
                    }
                    // acc.acc(res.receiverId, res.Amount);
                }
                if (evt.Event.EventType.Equals("CashWithdrawn"))
                {
                    var res = evt.Event.ParseJson<CashWithdrawn>();
                    if (res.AccountId == id)
                    {
                        acc.SetState(res);
                        verif = true;
                    }
                    // acc.acc(res.receiverId, res.Amount);
                }
            }
            if (verif) return acc;
            return null;
        }

        public void SaveEvents(AggregateRoot agg)
        {
            foreach(var evt in agg.events)
            {
                _conn.AppendToStreamAsync("AccountBalance", ExpectedVersion.Any, evt.AsJson()).Wait();
            }
        }
    }
}
