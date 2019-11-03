namespace Gucm.Domain.Models
{
    public sealed class BusinessError
    {

        protected BusinessError() { }

        public BusinessError(string property, string rule)
        {
            this.Property = property ?? throw new System.ArgumentNullException(nameof(property));
            this.Rule = rule ?? throw new System.ArgumentNullException(nameof(rule));
        }

        public BusinessError(string rule)
        {
            this.Rule = rule ?? throw new System.ArgumentNullException(nameof(rule));
        }

        public string Property { get; }

        public string Rule { get; }
    }
}
