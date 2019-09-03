using ReactiveDomain.Foundation;
using ReactiveDomain.Messaging.Bus;
using System;
using System.Collections.Generic;

namespace TESTDICT
{
    public class ReadModel : ReadModelBase, IHandle<OrderCreated>
    {
        public string history;
        public Dictionary<string, OrderDetail> Assets = new Dictionary<string, OrderDetail>();
        public decimal Total;

        public ReadModel(Func<IListener> listener) : base("", listener)
        {
            EventStream.Subscribe<OrderCreated>(this);
            Start<Order>(null, true);
        }

        public void Handle(OrderCreated message)
        {
            history += "Order Created : " + message.Action + " " + message.Quantite + " " + message.Asset + " With Unit Price :" + message.Price+" \n";

            if (message.Action.ToLower().Equals("buy"))
            ActionBuy(message);
            if (message.Action.ToLower().Equals("sell"))
            ActionSell(message);
        }

        private void ActionSell(OrderCreated message)
        {
            var detail = new OrderDetail(message.Price, message.Quantite);

            if (!Assets.TryGetValue(message.Asset, out OrderDetail v))
                throw new InvalidOperationException("Asset Does Not Existe !");

            if (message.Quantite > Assets[message.Asset].Quantite) throw new InvalidOperationException("Cannot make the sell happend");

            Assets[message.Asset].Quantite -= message.Quantite;
            Total = Total - (message.Quantite * Assets[message.Asset].price);

        }

        public void ActionBuy(OrderCreated message)
        {
            var detail = new OrderDetail(message.Price, message.Quantite);

            if (Assets.TryGetValue(message.Asset,out OrderDetail v))
            {
                Assets[message.Asset].Quantite += message.Quantite;
                Total = Total + message.Quantite * Assets[message.Asset].price;
            }
            else
            {
                Assets.Add(message.Asset,detail);
                Total = Total + message.Quantite * message.Price;
            }


        }

      
    }
}
