
using ReactiveDomain.Messaging;
using System;

namespace TESTDICT
{
    public class OrderCreated : Message
    {
        public Guid OrderId;
        public string Action;
        public string Asset;
        public decimal Price;
        public int Quantite;

        public OrderCreated(Guid id,string act,string asset,decimal price,int qts)
        {
            this.OrderId = id;
            this.Action = act;
            this.Asset = asset;
            this.Price = price;
            this.Quantite = qts;
        }
    }
}
