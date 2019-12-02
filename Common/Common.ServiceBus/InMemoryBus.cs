using Common.Infrastructure.Bus;
using Common.Infrastructure.Comands;
using Common.Infrastructure.Events;
using MediatR;
using System.Threading.Tasks;

namespace Common.ServiceBus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
