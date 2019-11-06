using Common.Infrastructure.UnitOfWork;
using Gucm.Application.ViewModel;
using Gucm.Domain.Gdpr;
using Gucm.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application.Handlers
{

    public class DeleteGdprHandler : IRequestHandler<DeleteGdprCommand, BusinessResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IGdprDomainRepository _gdprDomainRepository;

        public DeleteGdprHandler(IUnitOfWork unitOfWork, IGdprDomainRepository gdprDomainRepository)
        {
            _unitOfWork = unitOfWork;

            _gdprDomainRepository = gdprDomainRepository;
        }

        public async Task<BusinessResult<bool>> Handle(DeleteGdprCommand request, CancellationToken cancellationToken)
        {
            var result = new BusinessResult<bool>();

            var updatedDomain = _gdprDomainRepository.FindBy(request.Id);
            if (updatedDomain == null)
            {
                result.AddBrokenRule(new BusinessError("Record does not exist"));
                return result;
            }

            _gdprDomainRepository.Remove(updatedDomain);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            result.Model = true;

            return result;
        }
    }
}
