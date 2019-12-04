
using Common.Infrastructure.Comands;

namespace Gucm.Application.ViewModel
{
    public abstract class GdprCommand : Command
    {
        public string Gdpr { get; set; }

        public virtual int Id { get; set; }
    }
}
