using Eventstore;
using EventStore.ClientAPI;
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
    public class products
    {
        public List<Result> Body = new List<Result>();
    }
    public class Result
    {
        public string ProductName { get; set; }
        public int Qts { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var store = new EventStoree();
            store.Connect();
            var Service = new Inventoryservice(store);
            Service.SubscribeValueChange(store);
            Service.Read(store);

            Console.WriteLine("Listening to new Events :");
            Console.Read();
        }
       



        }

    public class Inventoryservice
    {
        public products products = new products();
        public Inventoryservice(EventStoree store)
        {
        }
        public void SubscribeValueChange(EventStoree store)
        {
            store.Connection().SubscribeToStream("result", false, ValueChanged, Dropped, new UserCredentials("admin", "changeit"));
        }

        private void ValueChanged(EventStoreSubscription eventStoreSubscription, ResolvedEvent resolvedEvent)
        {
            /*
            var evt = resolvedEvent.Event.ParseJson<SaleAchieved>();
            sum = sum + (evt.Qts * evt.Price);
            Console.Clear();
            */
            Console.WriteLine("New Value : ");
            var evt = resolvedEvent.Event.ParseJson<products>();
            Console.WriteLine("***************************************************");
            products.Body.Add(evt.Body[evt.Body.Count - 1]);
            Console.WriteLine(evt.Body[evt.Body.Count - 1].ProductName + " " + evt.Body[evt.Body.Count - 1].Qts);
            Show();

        }

        private void Dropped(EventStoreSubscription subscription, SubscriptionDropReason subscriptionDropReason, Exception exception)
        {
        }

        public void Read(EventStoree store)
        {
            var result = store.Connection().ReadStreamEventsForwardAsync("salesStream", 0,
                                                              100, false)
                                                              .Result;
            foreach (var evt in result.Events)
            {
                var sale = evt.Event.ParseJson<SaleAchieved>();
                var Res = new Result { ProductName = sale.ProductName, Qts = sale.Qts };
                products.Body.Add(Res);
            }
            Show();
        }

        private void Show()
        {
            Console.WriteLine("***************************************************");
            var result = from p in products.Body
                         group p by p.ProductName into g
                         select new
                         {
                             Name = g.Key,
                             Qts = g.Sum(x => x.Qts),
                         };

            foreach (var elem in result)
            {
                Console.WriteLine(elem.Name + " " + elem.Qts);
            }
        }



    }
}

