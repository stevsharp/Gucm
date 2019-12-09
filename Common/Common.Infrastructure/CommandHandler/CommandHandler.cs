using Common.Infrastructure.Bus;
using Common.Infrastructure.Comands;
using Common.Infrastructure.Notifications;
using Common.Infrastructure.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application
{
    public class CommandHandler
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMediatorHandler _bus;
        protected readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }


        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        public async Task<bool> Commit(CancellationToken cancellationToken)
        {
            if (_notifications.HasNotifications()) return false;

            if (await _uow.SaveChangesAsync(cancellationToken) > 0) return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));

            return false;
        }
    }
}
