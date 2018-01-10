using System;
using System.Runtime.InteropServices.ComTypes;
using Debcore.Model;
using MongoDB.Driver;

namespace Data
{
    public class MongoDb : IMongoDb
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Data => Client.GetDatabase("deb");

        //todo adjust Product model and implement other collections
        public IMongoCollection<Product> Products => Data.GetCollection<Product>("products");

        //todo implement generic CRUD methods

        public MongoDb(string connectionString)
        {
            Client = new MongoClient(connectionString);
        }
    }
}