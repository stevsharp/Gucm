using Common.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gucm.Application.Events
{
    public class GdprCreated : Event
    {
        public GdprCreated(int id )
        {
            Id = id;
        }

        public int Id { get; }
    }
}
