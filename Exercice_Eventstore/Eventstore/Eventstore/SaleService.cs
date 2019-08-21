using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace Eventstore
{
    public class SaleService
    {
        private IEventStoreConnection _conn;
        private string _address;
        private int _port;
        private decimal _total;
        private IList<SaleAchieved> _list ; 

       

        public decimal getTotal()
        {
            return _total;
        }

        public SaleService(string address,int port)
        {
            _address = address;
            _port = port;
            Connect();
            _total = TotalSales();
            _list = GetEvents("salesStream");
        }

        public SaleService()
        {
            var address = Environment.GetEnvironmentVariable("ES_address", EnvironmentVariableTarget.Machine);
            var ip = Environment.GetEnvironmentVariable("ES_PORT", EnvironmentVariableTarget.Machine);
            if (ip == null || address == null) throw new InvalidOperationException("Variables de environement does not Exist !");

            _address = address; 
            _port = int.Parse(ip);
            Connect();
            _total = TotalSales();
            _list = GetEvents("salesStream");
        }

        public void Connect()
        {
            _conn = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse(_address), _port));
            _conn.ConnectAsync().Wait();
        }
        

        public void SubscribeValueChange(string eventName)
        {
            _conn.SubscribeToStream(eventName, false, ValueChanged, Dropped, new UserCredentials("admin", "changeit"));
        }

        private void ValueChanged(EventStoreSubscription eventStoreSubscription, ResolvedEvent resolvedEvent)
        {
            var evt = resolvedEvent.Event.ParseJson<SaleAchieved>();
            _list.Add(evt);

            _total = _total + (evt.Qts * evt.Price);
        }

        private void Dropped(EventStoreSubscription subscription, SubscriptionDropReason subscriptionDropReason, Exception exception)
        {
            SubscribeValueChange("salesStream");
        }


        // Write an event to eventstore ()
        public bool WriteEvent(string name,int qts , decimal price)
        {

            if (string.IsNullOrEmpty(name) || qts <= 0 || price <= 0) throw new InvalidOperationException("Invalid Operation!");

                var Event = new SaleAchieved(Guid.NewGuid(), Guid.NewGuid(), name, qts, price);
                _conn.AppendToStreamAsync("salesStream", ExpectedVersion.Any, Event.AsJson()).Wait();
                return true;
        
        }

        // Get events from eventstore
        private IList<SaleAchieved> GetEvents(string Stream)
        {
            IList<SaleAchieved> list = new List<SaleAchieved>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice =
                _conn.ReadStreamEventsForwardAsync(Stream, nextSliceStart,
                                                              1, false)
                                                              .Result;

                nextSliceStart = (int)currentSlice.NextEventNumber;

                foreach (var evt in currentSlice.Events)
                {
                    var res = evt.Event.ParseJson<SaleAchieved>();
                    list.Add(res);
                }
            } while (!currentSlice.IsEndOfStream);
 
            return list;
            

        }

        // list of products and quantites sold

        public IEnumerable<Result> GetProductsSold()
        {
            var result = from p in _list
                         group p by p.ProductName into g
                         select new Result
                         {
                             Name = g.Key,
                             Qts = g.Sum(x => x.Qts),
                         };

            return result;
        }

        // get the total of amount of sales (price * quantite) 
        private decimal TotalSales()
        {
            decimal total = 0; 
            var list = GetEvents("salesStream");
            foreach(var elem in list)
            {
                total += (elem.Price * elem.Qts);
            }

            return total;
        }


    }

    public class Result
    {
        public string Name;
        public int Qts;
    }
}
