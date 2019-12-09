using Common.Infrastructure.Bus;
using Common.Infrastructure.Notifications;
using Common.Infrastructure.UnitOfWork;
using Gucm.Application.ViewModel;
using Gucm.Domain.Gdpr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application.Handlers
{
    public class UpdateGdprHandler : CommandHandler,  IRequestHandler<UpdateGdprCommand, bool>
    {
        private readonly IGdprDomainRepository _gdprDomainRepository;

        public UpdateGdprHandler(IUnitOfWork uow, IGdprDomainRepository gdprDomainRepository,
            IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _gdprDomainRepository = gdprDomainRepository;
        }

        public async Task<bool> Handle(UpdateGdprCommand request, CancellationToken cancellationToken)
        {
            var commandIsValid = request.IsValid();

            if (!commandIsValid)
            {
                NotifyValidationErrors(request);
                return false;
            }

            var updatedDomain = _gdprDomainRepository.FindBy(request.Id);
            if (updatedDomain == null)
            {
                request.ValidationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("DB Entry", "Record not found"));

                NotifyValidationErrors(request);
                return false;
            }

            updatedDomain.UpdateFields(request.Id, request.Gdpr);

            var bcErrors = updatedDomain.GetBrokenRules();
            if (bcErrors.Count > 0)
            {
                foreach (var errBc in bcErrors)
                    request.ValidationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(errBc.Rule, errBc.Property));

                NotifyValidationErrors(request);
                return false;
            }

            _gdprDomainRepository.Update(updatedDomain);

            if (await Commit(cancellationToken))
            {
                // Raise Event

            }

            return true;

        }
    }
}
