using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DDD.DomainModel;
using EventStore.ClientAPI;

namespace DDD.DomainService
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        private IEventStoreConnection _conn;
        private string _address;
        private int _port;

        public Repository()
        {
            _address = "127.0.0.1";
            _port = 1113;
            _conn = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse(_address), _port));
            _conn.ConnectAsync().Wait();
        }
        public T GetbyID(Guid id)
        {
            throw new NotImplementedException();
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
