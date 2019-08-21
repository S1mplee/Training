using System;

namespace Eventstore
{
    public class SaleAchieved : Event
    {
        public Guid ProductId;
        public string ProductName { get; set; }
        public int Qts { get; set; }
        public decimal Price { get; set; }

        public SaleAchieved(Guid id,Guid productId, string name, int qts, decimal price)
        {
            this.Id = id;
            this.ProductId = productId;
            this.ProductName = name;
            this.Qts = qts;
            this.Price = price;
        }

        public override string ToString()
        {
            return "" + Id + " " + " " + ProductName + " " + Price + " " + Qts;
        }

        public override bool Equals(object obj)
        {
            var ev = (SaleAchieved)obj;
            if (this.Id.Equals(ev.Id) && this.ProductName == ev.ProductName && this.Price == ev.Price && this.Qts == ev.Qts) return true;
            return false;
        }

        
    }
}
