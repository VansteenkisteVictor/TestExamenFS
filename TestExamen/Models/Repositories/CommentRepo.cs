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
    public class CommentRepo : ICommentRepo
    {
        private readonly CommentsDBContext commentsDBContext;
        private readonly IMongoCollection<Comment> collection;
        public CommentRepo(CommentsDBContext commentsDBContext)
        {
            this.commentsDBContext = commentsDBContext;
            collection = commentsDBContext.Database.GetCollection<Comment>("comments");
        }
        //*** GET ------------------------------------------------------------- 
        public async Task<IEnumerable<Comment>> GetAll()
        {
            try
            {
                //1. docs ophalen 
                IMongoCollection<Comment> collection = commentsDBContext.Database.GetCollection<Comment>("comments");
                //2. docs bevragen (Mongo query) en returnen //noot: alle mongo methodes bestaan synchroon en asynchroon 
                var result = await collection.Find(FilterDefinition<Comment>.Empty).ToListAsync<Comment>();
                //3. Return query resultaat 
                return result;
            }
            catch (Exception exception) { throw exception; }
        }

        //Alternatief: gebruik van he tBson document ipv de entiteit 
        public async Task<List<BsonDocument>> GetAll_docs()
        {
            IMongoCollection<BsonDocument> collectionBson = commentsDBContext.Database.GetCollection<BsonDocument>("comments");
            var resultBson = await collectionBson.Find(_ => true).ToListAsync<BsonDocument>();
            return resultBson;
        }

        //het id kon ook vh type “ObjectId” zijn. Dit is het MongoId type // -> hier onnodig door de dataannotatie 
        public async Task<Comment> Get(string id)
        {
            var comment = await collection.Find(b => b.Id == id).FirstOrDefaultAsync<Comment>();
            return comment;
        }
        //*** CREATE ------------------------------------------------------------- 
        public async Task<Comment> CreateAsync(Comment c)
        {
            await commentsDBContext.Comments.InsertOneAsync(c);
            //commentsDBContext.Database.GetCollection<Comment>("boeken").InsertOne(c);
            return c;
        }
    }
}
