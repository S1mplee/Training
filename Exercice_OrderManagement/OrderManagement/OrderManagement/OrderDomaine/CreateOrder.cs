using ReactiveDomain.Messaging;
using System;

namespace TESTDICT
{
    public class CreateOrder : Command
    {
        public Guid OrderId;
        public string Action;
        public string Asset;
        public decimal Price;
        public int Quantite;

        public CreateOrder(Guid id, string act, string asset, decimal price, int qts)
        {
            this.OrderId = id;
            this.Action = act;
            this.Asset = asset;
            this.Price = price;
            this.Quantite = qts;
        }
    }
}