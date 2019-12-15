using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using MongoDB.Bson;

namespace Models.Repositories
{
    public interface IDetailRepo
    {
        Task<CommentDetails> CreateAsync(CommentDetails c);
        Task<CommentDetails> Get(ObjectId id);
        Task<IEnumerable<CommentDetails>> GetAll();
        Task<List<BsonDocument>> GetAll_docs();
    }
}