using Gucm.Data.EntityFramework;
using Gucm.Domain.Gdpr;

namespace Gucm.Data.Repository
{
    public class GdprDomainRepository : BaseEfRepository<GdprDomain> , IGdprDomainRepository
    {
        public GdprDomainRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
