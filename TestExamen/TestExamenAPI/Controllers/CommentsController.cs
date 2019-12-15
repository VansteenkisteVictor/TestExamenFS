using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Repositories;

namespace TestExamenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepo _repo;
        public CommentsController(ICommentRepo repo)
        {
            _repo = repo;
        }
        // GET: api/Comments
        [HttpGet(Name = "GetComments")]
        public async Task<IActionResult> Get()
        {
            var comments = _repo.GetAll();
            return Ok(comments.Result);
        }

        // GET: api/Comments/5
        [HttpGet("{id}", Name = "GetComment")]
        public async Task<IActionResult> Get(string id)
        {
            var comment = _repo.Get(id);
            return Ok(comment.Result);
        }

        // POST: api/Comments
        [HttpPost]
        public IActionResult Post([FromBody] Comment comment)
        {
            _repo.CreateAsync(comment);
            return Ok();
        }

        // PUT: api/Comments/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
