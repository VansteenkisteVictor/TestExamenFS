using Models;
using Models.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Repositories
{
    public class DetailRepo : IDetailRepo
    {
        private readonly CommentsDBContext commentsDBContext;
        public DetailRepo(CommentsDBContext commentsDBContext)
        {
            this.commentsDBContext = commentsDBContext;
        }
        //*** GET ------------------------------------------------------------- 
        public async Task<IEnumerable<CommentDetails>> GetAll()
        {
            try
            {
                //1. docs ophalen 
                IMongoCollection<CommentDetails> collection = commentsDBContext.Database.GetCollection<CommentDetails>("details");
                //2. docs bevragen (Mongo query) en returnen //noot: alle mongo methodes bestaan synchroon en asynchroon 
                var result = await collection.Find(FilterDefinition<CommentDetails>.Empty).ToListAsync<CommentDetails>();
                //3. Return query resultaat 
                return result;
            }
            catch (Exception exception) { throw exception; }
        }

        //Alternatief: gebruik van he tBson document ipv de entiteit 
        public async Task<List<BsonDocument>> GetAll_docs()
        {
            IMongoCollection<BsonDocument> collectionBson = commentsDBContext.Database.GetCollection<BsonDocument>("details");
            var resultBson = await collectionBson.Find(_ => true).ToListAsync<BsonDocument>();
            return resultBson;
        }

        //het id kon ook vh type “ObjectId” zijn. Dit is het MongoId type // -> hier onnodig door de dataannotatie 
        public async Task<CommentDetails> Get(ObjectId id)
        {
            var comment = await commentsDBContext.Details.Find(b => b.Id == id).FirstOrDefaultAsync<CommentDetails>();
            return comment;
        }
        //*** CREATE ------------------------------------------------------------- 
        public async Task<CommentDetails> CreateAsync(CommentDetails c)
        {
            await commentsDBContext.Details.InsertOneAsync(c);
            //commentsDBContext.Database.GetCollection<Comment>("boeken").InsertOne(c);
            return c;
        }
    }
}
