using System;
using System.Collections.Generic;
using System.Text;

namespace Gucm.Domain.Models
{
    public enum BusinessResultStatus
    {
        Success,
        Fail
    }

    public class BusinessResult
    {
        public List<BusinessError> BrokenRules { get; } = new List<BusinessError>();
        public BusinessResultStatus Status => BrokenRules.Count > 0 ? BusinessResultStatus.Fail : BusinessResultStatus.Success;

        public virtual bool IsSuccess() => Status == BusinessResultStatus.Success;

        public void AddBrokenRule(BusinessError brokenRule) => BrokenRules.Add(brokenRule);

        public void AddBrokenRule(IEnumerable<BusinessError> brokenRules) => BrokenRules.AddRange(brokenRules);

        public static implicit operator BusinessResult(BusinessError businessRule) => SingleErrorResult(businessRule);
        public static implicit operator BusinessResult(List<BusinessError> businessRules) => ErrorResult(businessRules);

        public static BusinessResult SingleErrorResult(string message) => SingleErrorResult(new BusinessError(string.Empty, message));

        public static BusinessResult SingleErrorResult(string property, string message) => SingleErrorResult(new BusinessError(property, message));

        public static BusinessResult SingleErrorResult(BusinessError businessRule)
        {
            var result = new BusinessResult();
            result.AddBrokenRule(businessRule);

            return result;
        }
        public static BusinessResult ErrorResult(IEnumerable<BusinessError> businessRules)
        {
            var result = new BusinessResult();
            result.AddBrokenRule(businessRules);

            return result;
        }
    }

    public class BusinessResult<TModel> : BusinessResult
    {
        public TModel Model { get; set; }

        public BusinessResult() { }

        public BusinessResult(TModel model) => Model = model;

        public static implicit operator BusinessResult<TModel>(TModel model) => new BusinessResult<TModel>(model);
        public static implicit operator BusinessResult<TModel>(BusinessError businessRule) => SingleErrorResult(businessRule);
        public static implicit operator BusinessResult<TModel>(List<BusinessError> businessRules) => ErrorResult(businessRules);

        public new static BusinessResult<TModel> SingleErrorResult(string message) => SingleErrorResult(new BusinessError(string.Empty, message));

        public new static BusinessResult<TModel> SingleErrorResult(string property, string message) => SingleErrorResult(new BusinessError(property, message));

        public new static BusinessResult<TModel> SingleErrorResult(BusinessError businessRule)
        {
            var result = new BusinessResult<TModel>();
            result.AddBrokenRule(businessRule);

            return result;
        }

        public new static BusinessResult<TModel> ErrorResult(IEnumerable<BusinessError> businessRules)
        {
            var result = new BusinessResult<TModel>();
            result.AddBrokenRule(businessRules);

            return result;
        }
    }
}
