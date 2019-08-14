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
        }

        public bool WriteEvent(string name,int qts , decimal price)
        {
            var ev = Sale.Create(Guid.NewGuid(), name,qts, price);
            _conn.ConnectAsync().Wait();
            _conn.AppendToStreamAsync("SalesStream", ExpectedVersion.Any, ev.AsJson()).Wait();
            return true;
        }

    }
}
