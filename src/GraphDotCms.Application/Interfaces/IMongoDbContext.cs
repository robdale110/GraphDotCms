using MongoDB.Driver;

namespace GraphDotCms.Application.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
