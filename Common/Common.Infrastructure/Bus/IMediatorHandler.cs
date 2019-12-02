using Common.Infrastructure.Comands;
using Common.Infrastructure.Events;
using System.Threading.Tasks;

namespace Common.Infrastructure.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
