using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Director
{
    class Program
    {
        static void Main(string[] args)
        {
            GetProducts();
            Console.ReadLine();
        }
        public static async void GetProducts()
        {
            var conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            conn.ConnectAsync().Wait();

            ProjectionsManager projectionsManager = new ProjectionsManager(new ConsoleLogger(), new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2113), TimeSpan.FromMilliseconds(5000));


            while (true)
            {
                var readEvents = conn.ReadStreamEventsForwardAsync("Test", 0, 10, true)
          .Result;
                var projectionState = await projectionsManager.GetStateAsync("Test", new UserCredentials("admin", "changeit"));
                dynamic salesNumber = JsonConvert.DeserializeObject(projectionState);
                Newtonsoft.Json.Linq.JArray tab = salesNumber.Body;

                var result = from p in tab
                             select new { name = (string)p["ProductName"], value = int.Parse((string)p["Qts"]) };

                var result2 = from p in result
                              group p by p.name into g
                              select new Result
                              {
                                  name = g.Key,
                                  qts = g.Sum(x => x.value),
                              };

                foreach (var elem in result2)
                {
                    Console.Write(elem.name + " ");
                    Console.Write(elem.qts);
                    Console.WriteLine();
                }
                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        public class Result
        {
            public string name;
            public int qts;
        }
    }
}

