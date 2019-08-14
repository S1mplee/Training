using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eventstore
{
    public class SaleService
    {
        private IEventStoreConnection _conn;
        private string _address;
        private int _port;

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
            var ev = Sale.Create(Guid.NewGuid(), name,qts, price);
            _conn.AppendToStreamAsync("SalesStream", ExpectedVersion.Any, ev.AsJson()).Wait();
            return true;
        }

        // Get events from eventstore
        public IList<SaleAchieved> GetEvents(string Stream)
        {
            IList<SaleAchieved> list = new List<SaleAchieved>();

            var readEvents = _conn.ReadStreamEventsForwardAsync(Stream, 1, 100, true).Result;
            foreach (var evt in readEvents.Events)
            {
                var res = evt.Event.ParseJson<SaleAchieved>();
                list.Add(res);
            }

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


    }

    public class Result
    {
        public string Name;
        public int Qts;
    }
}
