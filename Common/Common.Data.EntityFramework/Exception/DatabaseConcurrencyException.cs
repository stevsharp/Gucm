using System;

namespace Gucm.Data.EntityFramework
{
    public class DatabaseConcurrencyException : Exception
    {
        public DatabaseConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
