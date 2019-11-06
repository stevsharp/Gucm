using Common.Domain.Interface;
using Common.Domain.Models;
using Common.Infrastructure.Validation;
using System;

namespace Gucm.Domain.Gdpr
{
    public class GdprDomain : EntityBase, IAggregateRoot
    {
        private GdprDomain() {}

        public GdprDomain(string Gdpr)
        {
            Check.That(!string.IsNullOrWhiteSpace(Gdpr), () => { throw new ArgumentException("Gdpr cannot be null."); });

            this.Gdpr = Gdpr;
        }

        public virtual string Gdpr { get; private set; }

        public void UpdateFields(int inputId , string inputGdpr)
        {
            Check.That(inputId != 0, () => { throw new ArgumentException("Gdpr cannot be null."); });

            Check.That(!string.IsNullOrWhiteSpace(inputGdpr), () => { throw new ArgumentException("Gdpr cannot be null."); });

            this.Id = inputId;

            this.Gdpr = inputGdpr;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Validate()
        {
            // Add BC rules 
        }
    }
}
