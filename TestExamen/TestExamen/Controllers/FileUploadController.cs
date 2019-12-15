using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TestExamen.Hubs;

namespace TestExamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IHubContext<CommentHub> _hubContext;
        public FileUploadController(IHubContext<CommentHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public class JsonDTO { public string fileName { get; set; } = null; public string formFile { get; set; } }

        [HttpPost] 
        [Route("UploadFileByJS")] 
        public async Task<IActionResult> UploadByJS([FromBody] JsonDTO jsonDTO) 
        { 
            if (ModelState.IsValid) 
            { 
                if (jsonDTO.formFile == null || jsonDTO.formFile.Length == 0) 
                    return Content("file not selected"); 
                await _hubContext.Clients.All.SendAsync("UserImage", jsonDTO.formFile); } 
            return Accepted(new { body = jsonDTO }); }
    }
}