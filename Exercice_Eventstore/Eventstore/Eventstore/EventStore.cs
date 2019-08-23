using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eventstore
{
    public class EventStoree
    {
        private IEventStoreConnection _conn;
        private string _address;
        private int _port;

        public EventStoree()
        {
            var address = Environment.GetEnvironmentVariable("ES_address", EnvironmentVariableTarget.Machine);
            var ip = Environment.GetEnvironmentVariable("ES_PORT", EnvironmentVariableTarget.Machine);
            if (ip == null || address == null) throw new InvalidOperationException("Variables de environement does not Exist !");

            _address = address;
            _port = int.Parse(ip);

        }

        public void  Connect()
        {
            _conn = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse(_address), _port));
            _conn.ConnectAsync().Wait();
        }

        public IEventStoreConnection Connection()
        {
            return _conn;
        }
    }

}
