using GraphDotCms.Application.Interfaces;
using MongoDB.Driver;

namespace GraphDotCms.Persistence.Contexts
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public MongoDbContext(string connectionString, string databaseName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) =>
            Database.GetCollection<T>(collectionName);
    }
}
