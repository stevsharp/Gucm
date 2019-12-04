using System;

namespace Common.Data.MongoCollection
{

    public sealed class RepositoryOptions
    {
        protected RepositoryOptions() { }
        public RepositoryOptions(string connectionString, string databaseName, string collectionName)
        {
            CollectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));

            DatabaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));

            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public readonly string ConnectionString;

        public readonly string CollectionName;

        public readonly string DatabaseName;
    }
}
