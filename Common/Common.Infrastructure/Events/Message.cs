using Gucm.Domain.Models;
using MediatR;
using System;

namespace Common.Infrastructure
{
    public abstract class Message : IRequest<BusinessResult<bool>>
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
