using ReactiveDomain;
using ReactiveDomain.Messaging;
using System;

namespace TESTDICT
{
    public class Order : EventDrivenStateMachine
    {
        private string Action;
        private string Asset;
        private decimal Price;
        private int Quantite;
        public Order()
        {
            Register<OrderCreated>(evt =>
            {
                this.Id = evt.OrderId;
                this.Action = evt.Action;
                this.Asset = evt.Asset;
                this.Price = evt.Price;
                this.Quantite = evt.Quantite;
            });
        }
        public Order(Guid id,string action,string asset,decimal price,int qts) : this()
        {
            if (id == null || string.IsNullOrEmpty(action) || string.IsNullOrEmpty(asset) || price <= 0 || qts <= 0) throw new ArgumentException("Invalid Inputs !");
            Raise(new OrderCreated(id, action, asset, price, qts));
        }

     class MySecretEvent : Message { }

    }
}
