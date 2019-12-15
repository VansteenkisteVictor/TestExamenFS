using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestExamen.Helpers
{
    public class CollectionExists
    {

        //public async Task<bool> CollectionExistsAsync(string collectionName)
        //{
        //    var filter = new BsonDocument("name", collectionName); 
        //    //filter by collection name 
        //    var collections = await Database.ListCollectionsAsync( new ListCollectionsOptions { Filter = filter });
        //    //check for existence 
        //    return await collections.AnyAsync(); 
        //}
    }
}
