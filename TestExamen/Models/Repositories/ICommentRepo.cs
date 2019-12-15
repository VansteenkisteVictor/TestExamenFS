using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using MongoDB.Bson;

namespace Models.Repositories
{
    public interface ICommentRepo
    {
        Task<Comment> CreateAsync(Comment c);
        Task<Comment> Get(string id);
        Task<IEnumerable<Comment>> GetAll();
        Task<List<BsonDocument>> GetAll_docs();
    }
}