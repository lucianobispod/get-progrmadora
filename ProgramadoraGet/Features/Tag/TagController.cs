using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;

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

        [Authorize]
        [HttpPost]
        public async Task<Domain.Tag> Create([FromBody] Create.Model model)
        {
            return await new Create.Services(db).Save(model); 
        }
        
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IList<Domain.Tag>> Read(Read.Model model)
        {
            return await new Read.Services(db).One(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IList<Domain.Tag>> ReadByTagType(string tagType)
        {
            object tagTypeOut = null;
            if (!Enum.TryParse(typeof(Domain.TagType), tagType, out tagTypeOut))
            {
                throw new Exception("TagType inválido");
            }

            Tag.Read.Model tg = new Tag.Read.Model();
            tg.TagType = (Domain.TagType) tagTypeOut;

            return await new Read.Services(db).ByTagType(tg);

        }

    }
}
