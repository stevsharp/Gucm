using Common.Domain.Interface;
using Common.Infrastructure.Events;
using System;

namespace Gucm.Application.Events
{
    public class GdprCreated : Event
    {
        public GdprCreated(int id )
        {
            Id = id;

            this.AggregateId = Guid.NewGuid();
        }

        public int Id { get; }
    }
}
