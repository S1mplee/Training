using EventStore.ClientAPI;
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

        public SaleService(string address,int port)
        {
            _address = address;
            _port = port;
        }

        public SaleService()
        {
            _address = "127.0.0.1";
            _port = 1113;
            _conn = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse(_address), _port));
            _conn.ConnectAsync().Wait();
        }

        // Write an event to eventstore ()
        public bool WriteEvent(string name,int qts , decimal price)
        {
            var Event = Sale.Create(Guid.NewGuid(), name,qts, price);
            _conn.AppendToStreamAsync("SalesStream", ExpectedVersion.Any, Event.AsJson()).Wait();
            return true;
        }

        // Get events from eventstore
        public IList<SaleAchieved> GetEvents(string Stream)
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
            var list = this.GetEvents("SalesStream");
            var result = from p in list
                         group p by p.ProductName into g
                         select new Result
                         {
                             Name = g.Key,
                             Qts = g.Sum(x => x.Qts),
                         };

            return result;
        }

        // get the total of amount of sales (price * quantite) 
        public decimal TotalSales()
        {
            decimal total = 0; 
            var list = GetEvents("SalesStream");
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
