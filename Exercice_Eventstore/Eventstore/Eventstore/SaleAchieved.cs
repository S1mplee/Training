using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventstore
{
    public class SaleAchieved
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
    }
}
