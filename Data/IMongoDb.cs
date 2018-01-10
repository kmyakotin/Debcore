using System;
using System.Threading.Tasks;
using Debcore.Model;
using MongoDB.Driver;

namespace Data
{
    public interface IMongoDb
    {
        IMongoClient Client { get; }
        IMongoDatabase Data { get; }
        IMongoCollection<Party> Parties { get; }
        Task<Party> GetParty(string name);
        Task<Party> SaveParty(Party party);
        Task DeleteParty(Guid partyId);
    }
}