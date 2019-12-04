using MongoDB.Driver;
using System;

namespace Common.Data.MongoCollection
{
    public class DbFactory : IDbFactory
    {
        public MongoClient dbClient { get; protected set; }
        public IMongoDatabase GetDatabase(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            dbClient = new MongoClient(connectionString);

            var db = dbClient.GetDatabase(databaseName);

            return db;
        }
    }
}
