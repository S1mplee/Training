using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD
{
    public interface IEventHandler<T> where T : Event
    {
        void Handle(T evt);
    }
}
