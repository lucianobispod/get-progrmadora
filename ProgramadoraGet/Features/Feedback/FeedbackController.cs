using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Feedback
{
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly Db db;

        public FeedbackController(Db db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost]
        public async Task<Domain.Feedback> Create([FromBody] Create.Model model)
        {
            model.UserId = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            return await new Create.Services(db).Save(model);
        }

        [HttpGet]
        public async Task<IList<Domain.Feedback>> ReadAll()
        {
            return await new Read.Services(db).All();

        }

    }
}
