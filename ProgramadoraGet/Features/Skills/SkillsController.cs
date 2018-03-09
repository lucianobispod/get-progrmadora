using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Skills
{
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private Db db;

        public SkillsController(Db db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<Domain.Skills> Create([FromBody] Create.Model model)
        {
            return await new Create.Services(db).Save(model);
        }


        [HttpDelete]
        public async Task Delete([FromBody] Delete.Model model)
        {
            await new Delete.Services(db).Trash(model);
        }


    }
}
