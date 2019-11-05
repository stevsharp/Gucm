using Gucm.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Common.Domain.Models
{
    public abstract class ValueObjectBase
    {
        private List<BusinessError> brokenRules = new List<BusinessError>();

        [NotMapped]
        public List<BusinessError> BrokenRules { get => brokenRules; set => brokenRules = value; }

        public abstract void Validate(string pname = null);

        public void ThrowExceptionIfInvalid()
        {
            if (brokenRules == null)
                brokenRules = new List<BusinessError>();

            BrokenRules.Clear();
            Validate();

            if (BrokenRules.Count() > 0)
            {
                StringBuilder issues = new StringBuilder();
                foreach (BusinessError businessRule in BrokenRules)
                    issues.AppendLine(businessRule.Rule);

                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }

        protected void AddBrokenRule(BusinessError businessRule)
        {
            BrokenRules.Add(businessRule);
        }

        protected static bool EqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            ValueObjectBase other = (ValueObjectBase)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        public ValueObjectBase GetCopy()
        {
            return this.MemberwiseClone() as ValueObjectBase;
        }

        public static bool operator ==(ValueObjectBase a, ValueObjectBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObjectBase a, ValueObjectBase b)
        {
            return !(a == b);
        }
    }
}
