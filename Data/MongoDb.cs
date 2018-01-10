using System;
using System.Diagnostics;
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

        //todo implement generic CRUD methods

        public MongoDb(string connectionString)
        {
            Client = new MongoClient(connectionString);
        }

        public async Task<Party> GetParty(string name)
        {
            return await (await Parties.FindAsync(x => x.Name == name)).SingleOrDefaultAsync();
        }

        public async Task<Party> SaveParty(Party party)
        {
            //todo ensure its null for insert
//            if (party.BsonId != null)
//            {
//                await Parties.InsertOneAsync(party);
//                return party;
//            }

//            var res = await Parties.ReplaceOneAsync(Builders<Party>.Filter.Eq(x => x.PartyId == party.PartyId, true),
//                party, new UpdateOptions() {IsUpsert = true});
            var res = await Parties.ReplaceOneAsync(new BsonDocument("partyId", party.PartyId), party,
                new UpdateOptions() {IsUpsert = true});
            Debug.Assert(res.UpsertedId == party.BsonId, "res.UpsertedId == party.BsonId");

            return party;
        }
    }
}