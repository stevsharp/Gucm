
using Common.Infrastructure.Comands;
using Gucm.Domain.Models;
using MediatR;

namespace Gucm.Application.ViewModel
{
    public abstract class GdprCommand : Command
    {
        public string Gdpr { get; set; }
    }
}
