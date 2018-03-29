using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<Domain.Skills> Create([FromBody] Create.Model model)
        {
            model.UserId = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            return await new Create.Services(db).Save(model);
        }

        [Authorize]
        [HttpDelete]
        public async Task Delete([FromBody] Delete.Model model)
        {
            model.UserId = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            await new Delete.Services(db).Trash(model);
        }


    }
}
