using Gucm.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application.EventHandler
{
    public class GdprEventHandler : INotificationHandler<GdprCreated>
    {
        public Task Handle(GdprCreated notification, CancellationToken cancellationToken)
        {


            return Task.CompletedTask;
        }
    }
}
