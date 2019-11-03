using System;

namespace Gucm.Data.EntityFramework
{

    public class DatabaseUpdateException : Exception
    {
        public DatabaseUpdateException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
