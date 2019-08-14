using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventstore
{
    public class Sale
    {
        private Guid _Id;
        private string _ProductName;
        private int _qts;
        private decimal _price;

        private Sale(Guid id, string name, int q, decimal p)
        {
            this._Id = id;
            this._ProductName = name;
            this._qts = q;
            this._price = p;
        }

        public static SaleAchieved Create(Guid id, string name, int q, decimal p)
        {
            if (id == null || string.IsNullOrWhiteSpace(name) || q <= 0 || p <= 0) throw new ArgumentException("Invalid Input !");

            new Sale(id, name, q, p);

            return new SaleAchieved(id, name, q, p);

        }

    }
}
