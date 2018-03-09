using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<Domain.Feedback> Create([FromBody] Create.Model model)
        {
            return await new Create.Services(db).Save(model);
        }

        //[HttpGet]
        //public async Task<IList<>> List()
        //{
        //    return await mediator.Send(new List.Query());

        //}

    }
}
