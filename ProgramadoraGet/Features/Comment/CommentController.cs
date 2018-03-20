using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Comment
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private Db db;

        public CommentController(Db db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost]
        public async Task<Create.DefaultResponse> Create([FromBody] Create.Model model)
        {
            model.UserId = Guid.Parse(User.Claims.Where(w => w.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);

            return await new Create.Services(db).Save(model);
        }
        
        [Authorize]
        [HttpDelete]
        [Route("{CommentId}")]
        public async Task<DateTime?> Delete(Delete.Model model)
        {
            model.MeIdentifier = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            return await new Delete.Services(db).Trash(model);
        }
              
        [Authorize]
        [HttpPut]
        [Route("{CommentId}")]
        public async Task<Domain.Comment> Update(Guid CommentId, [FromBody] Update.Model model)
        {
            model.CommentId = CommentId;
            return await new Update.Services(db).Save(model);
        }

       
    }
}
