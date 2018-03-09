using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Tag
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private Db db;

        public TagController(Db db)
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
