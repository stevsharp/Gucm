using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;

namespace Common.Data.MongoCollection
{

    public class DbFactory : IDbFactory
    {
        public IMongoDatabase GetDatabase(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var dbClient = new MongoClient(connectionString);

            var connStr = new ConnectionString(connectionString);
            var db = dbClient.GetDatabase(connStr.DatabaseName);

            return db;
        }
    }
}
