using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Technology
{
    [Route("api/[controller]")]
    public class TechnologyController : Controller
    {

        private Db db;

        public TechnologyController(Db db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<Domain.Tag> Create([FromBody] Create.Model model)
        {
            return await new Create.Services(db).Save(model);
        }


    }
}
