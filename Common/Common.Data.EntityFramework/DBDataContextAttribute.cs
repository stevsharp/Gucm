using System;

namespace Gucm.Data.EntityFramework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DBDataContextAttribute : Attribute
    {
        public string Description { get; }

        public DBDataContextAttribute(string description)
        {
            this.Description = description;
        }
    }
}
