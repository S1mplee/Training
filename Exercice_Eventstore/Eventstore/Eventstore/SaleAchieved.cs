using System;

namespace Eventstore
{
    public class SaleAchieved : Event
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Qts { get; set; }
        public decimal Price { get; set; }

        public SaleAchieved(Guid id, string name, int q, decimal p)
        {
            this.Id = id;
            this.ProductName = name;
            this.Qts = q;
            this.Price = p;
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
