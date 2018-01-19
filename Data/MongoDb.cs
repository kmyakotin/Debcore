using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Debcore.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data
{
    public class MongoDb : IMongoDb
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Data => Client.GetDatabase("deb");
        public IMongoCollection<Party> Parties => Data.GetCollection<Party>("party");

        public MongoDb(string connectionString)
        {
            Client = new MongoClient(connectionString);
        }

        public async Task<Party> GetParty(string name)
        {
            return (await GetParties(x => x.Name == name)).SingleOrDefault();
        }

        public async Task<IEnumerable<Party>> GetParties(Expression<Func<Party, bool>> whereExpression)
        {
            return await (await Parties.FindAsync(whereExpression)).ToListAsync();
        }

        public async Task<Party> SaveParty(Party party)
        {
            // todo test save always insert, because partyId = Guid.NewGuid(). test it
            var res = await Parties.ReplaceOneAsync(new BsonDocument("partyId", party.PartyId), party,
                new UpdateOptions() {IsUpsert = true});
            Debug.Assert(res.UpsertedId == party.BsonId, "res.UpsertedId == party.BsonId");

            return party;
        }

        public Task DeleteParty(Guid partyId)
        {
            // todo implement delete party
            throw new NotImplementedException(nameof(DeleteParty));
        }
    }
}