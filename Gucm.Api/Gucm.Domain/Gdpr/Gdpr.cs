using Common.Domain.Interface;
using Common.Domain.Models;
using Common.Infrastructure.Validation;
using System;

namespace Gucm.Domain.Gdpr
{
    public class GdprDomain : EntityBase, IAggregateRoot
    {

        protected GdprDomain() { }

        public GdprDomain(int newId , string Gdpr)
        {
            Check.That(newId == 0, () => { throw new ArgumentException("Id cannot be 0."); });
            Check.That(string.IsNullOrWhiteSpace(Gdpr), () => { throw new ArgumentException("Gdpr cannot be null."); });

            this.Id = newId;
            this.Gdpr = Gdpr;
        }

        public virtual string Gdpr { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected override void Validate()
        {
            // Add BC rules 
        }
    }
}
