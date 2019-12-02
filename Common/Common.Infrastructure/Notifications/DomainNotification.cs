using Common.Infrastructure.Events;
using System;

namespace Common.Infrastructure.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }

        public string Key { get; protected set; }

        public string Value { get; protected set; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}
