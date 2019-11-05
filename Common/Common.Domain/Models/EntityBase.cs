using Gucm.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Models
{
    public abstract class EntityBase
    {
        private List<BusinessError> _brokenRules = new List<BusinessError>();
        private int? _requestedHashCode;

        public virtual int Id { get; set; }

        protected void AddBrokenRule(BusinessError businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public IList<BusinessError> GetBrokenRules()
        {
            _brokenRules.Clear();

            Validate();

            return _brokenRules;
        }

        protected abstract void Validate();

        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            EntityBase item = (EntityBase)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(EntityBase left, EntityBase right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(EntityBase left, EntityBase right)
        {
            return !(left == right);
        }
    }
}
