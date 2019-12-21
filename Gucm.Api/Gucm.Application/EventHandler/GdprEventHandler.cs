using Gucm.Application.Events;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application.EventHandler
{
    public class GdprEventHandler : INotificationHandler<GdprCreated>
    {
        private readonly IBus _bus;

        public GdprEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(GdprCreated notification, CancellationToken cancellationToken)
        {
            await _bus.Publish<GdprCreated>(notification);   
        }
    }
}
