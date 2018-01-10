using Debcore.Model;
using MongoDB.Driver;

namespace Data
{
    public interface IMongoDb
    {
        IMongoClient Client { get; }
        IMongoDatabase Data { get; }
        IMongoCollection<Product> Products { get; }
    }
}