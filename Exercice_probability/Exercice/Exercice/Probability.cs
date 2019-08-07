using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice
{
    public struct Probability
    {
        private readonly decimal _value;

        private Probability(decimal v)
        {
            _value = v;
        }

        public static Probability Create(decimal v)
        {
            if (v < 0 || v > 1) throw new InvalidOperationException("Value Range is not correct");

            return new Probability(v);
        }



       public static bool Compare(Probability p,decimal d)
        {
            if (p._value == d) return true;

            return false;
        }

        public Probability And(Probability p)
        {
            var result = Probability.Create(p._value * this._value);
            return result;
        }

        public Probability Or(Probability p)
        {
            var result = Probability.Create(this._value + p._value - this._value * p._value);

            return result;
        }

        public Probability Inverse()
        {

            var result = Probability.Create(1- this._value);

            return result;
        }
    }
}
