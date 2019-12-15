using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Models.Data
{
    public class CommentsDBContext
    {
        public IMongoDatabase Database;

        public CommentsDBContext(IMongoSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<Comment> Comments => Database.GetCollection<Comment>("comments");
        public IMongoCollection<CommentDetails> Details => Database.GetCollection<CommentDetails>("details");

        //GridFS voor het chuncken van images
        public GridFSBucket ImagesBucket => new GridFSBucket(Database); 

        //helper functies op collectie niveau ------------------------------------------
        public async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = await Database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            //check for existence
            return await collections.AnyAsync();
        }


    }
}
