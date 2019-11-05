using Common.Infrastructure.UnitOfWork;
using Gucm.Application.ViewModel;
using Gucm.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application.Handlers
{
    public class CreateGdprHandler : IRequestHandler<CreateGdprCommand, BusinessResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGdprHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BusinessResult<bool>> Handle(CreateGdprCommand request, CancellationToken cancellationToken)
        {
            return new BusinessResult<bool>();
        }
    }
}
