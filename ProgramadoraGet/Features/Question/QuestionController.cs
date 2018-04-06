using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Question
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly Db db;

        public QuestionController(Db db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost]
        public async Task<Create.QuestionDefault> Create([FromBody] Create.Model model)
        {
            model.UserId = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            return await new Create.Services(db).Save(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IList<Read.QuestionDefault>> ReadAllQuestions()
        {
            return await new Read.Services(db).AllQuestions();
        }

        [Authorize]
        [HttpGet]
        [Route("{Id}")]
        public async Task<IList<Read.QuestionDefault>> ReadOneQuestion(Read.Model model)
        {
            return await new Read.Services(db).OneQuestion(model);
        }

        [Authorize]
        [HttpDelete]
        [Route("{QuestionId}")]
        public async Task<DateTime?> DeleteQuestion(Guid QuestionId)
        {
            return await new Delete.Services(db).Trash(new Delete.Model { MeIdentifier = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value), QuestionId = QuestionId });
        }

        [Authorize]
        [HttpPut]
        [Route("{QuestionIdentifier}")]
        public async Task<Update.QuestionDefault> Update(Guid QuestionIdentifier, [FromBody]Update.Model model)
        {
            model.UserIdentifier = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            model.QuestionIdentifier = QuestionIdentifier;
            return await new Update.Services(db).Save(model);
        }
    }
}
