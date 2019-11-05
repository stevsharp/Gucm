
using Gucm.Domain.Models;
using MediatR;
using System.Threading.Tasks;

namespace Gucm.Application.ViewModel
{
    public sealed class CreateGdprCommand : IRequest<BusinessResult<bool>>
    {
        public int Id { get; set; }

        public string Gdpr { get; set; }
    }
}
