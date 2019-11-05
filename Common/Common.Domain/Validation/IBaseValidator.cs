using Common.Domain.Models;
using Gucm.Domain.Models;
using System.Threading.Tasks;

namespace Common.Domain.Validation
{
    public interface IBaseValidator<T> where T : EntityBase
    {
        Task<BusinessResult> Validate(T entity);
    }
}
