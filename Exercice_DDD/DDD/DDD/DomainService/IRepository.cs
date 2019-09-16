
using DDD.DomainModel;
using System;

namespace DDD.DomainService
{
    public interface IRepository
    {
        void SaveEvents(AggregateRoot agg);
        AggregateRoot GetbyID(Guid id);
    }
}
