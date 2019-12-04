using MongoDB.Driver;

namespace Common.Data.MongoCollection
{
    public interface IDbFactory
    {
        IMongoDatabase GetDatabase(string connectionString, string databaseName);

        MongoClient dbClient { get; }
    }
}
