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
        private EventStoree repo;

        public List<Product> list = new List<Product>();

        public  void ChargeProducts()
        {
            list.Add(new Product { id = Guid.Parse("de6da6d0-17b6-491a-904b-c0781916781e"), ProductName = "CPU", price = 150 });
            list.Add(new Product { id = Guid.Parse("de4da6d0-17b6-491a-904b-c0781916781e"), ProductName = "GPU", price = 100 });
            list.Add(new Product { id = Guid.Parse("de8da6d0-17b6-491a-904b-c0781916781e"), ProductName = "Mouse", price = 50 });
            list.Add(new Product { id = Guid.Parse("de9da6d0-17b6-491a-904b-c0781916781e"), ProductName = "KeyBoard", price = 60 });

        }

        public void show()
        {
            int i = 0;
            foreach(var elem in list )
            {
                i++;
                Console.WriteLine(i+") Name : {0} Price : {1}",elem.ProductName,elem.price);
            }
        }





        public SaleService()
        {
            repo = new EventStoree();
            repo.Connect();
        }

        /*
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
                */

        // Write an event to eventstore ()
        public bool WriteEvent(Guid Pid,string name, int qts, decimal price)
        {

            if (string.IsNullOrEmpty(name) || qts <= 0 || price <= 0) throw new InvalidOperationException("Invalid Operation!");

            var Event = new SaleAchieved(Guid.NewGuid(), Pid, name, qts, price);
            repo.Connection().AppendToStreamAsync("salesStream", ExpectedVersion.Any, Event.AsJson()).Wait();
            return true;

        }

        /*    // Get events from eventstore
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
            */
        // get the total of amount of sales (price * quantite) 
        /*
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
    */
    }

}
