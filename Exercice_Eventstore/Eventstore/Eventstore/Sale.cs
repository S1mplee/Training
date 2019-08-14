using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventstore
{
    class Sale
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

    }
}
