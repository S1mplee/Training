
using DDD.DomainModel;

namespace DDD.DomainService
{
    interface IRepository<T> where T: AggregateRoot
    {
        void SaveEvents(AggregateRoot agg);
        AggregateRoot GetbyID(string id);
    }
}
