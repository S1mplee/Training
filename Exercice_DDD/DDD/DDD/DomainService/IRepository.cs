
using DDD.DomainModel;
using System;

namespace DDD.DomainService
{
    public interface IRepository<T> where T: AggregateRoot
    {
        void SaveEvents(AggregateRoot agg);
        AggregateRoot GetbyID(Guid id);
    }
}
