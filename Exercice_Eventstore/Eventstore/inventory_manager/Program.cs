using Eventstore;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_manager
{
    public class Program
    {
        static void Main(string[] args)
        {
            decimal sum = 0;
            Console.WriteLine("Director :");
            var store = new EventStoree();
            store.Connect();
            var Service = new service(store);
            Service.SubscribeValueChange(store);
            Console.WriteLine("Amount of Sales : " + Service.sum);
            Console.WriteLine("Listening to new Events :");
            Console.Read();
        }
        
    }

    public class service
    {
        public decimal sum;
        public service(EventStoree store)
        {
            var list = GetEvents(store);
             sum = TotalSales(list);
        }
        public void SubscribeValueChange(EventStoree store)
        {
            store.Connection().SubscribeToStream("salesStream", false, ValueChanged, Dropped, new UserCredentials("admin", "changeit"));
        }

        private void ValueChanged(EventStoreSubscription eventStoreSubscription, ResolvedEvent resolvedEvent)
        {
            var evt = resolvedEvent.Event.ParseJson<SaleAchieved>();
            sum = sum + (evt.Qts * evt.Price);
            Console.Clear();
            Console.WriteLine("New Value : " + sum);
        }

        private void Dropped(EventStoreSubscription subscription, SubscriptionDropReason subscriptionDropReason, Exception exception)
        {
        }

        private static IList<SaleAchieved> GetEvents(EventStoree store)
        {
            IList<SaleAchieved> list = new List<SaleAchieved>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice =
                store.Connection().ReadStreamEventsForwardAsync("salesStream", nextSliceStart,
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
        private static decimal TotalSales(IList<SaleAchieved> list)
        {
            decimal total = 0;
            foreach (var elem in list)
            {
                total += (elem.Price * elem.Qts);
            }

            return total;
        }


    }
}
