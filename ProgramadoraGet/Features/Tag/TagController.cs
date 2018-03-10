using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<Domain.Tag> Create([FromBody] Create.Model model)
        {
            return await new Create.Services(db).Save(model); 
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<DefaultResponse<IList<Domain.Tag>>> Read(Read.Model model)
        {
            var response = new DefaultResponse<IList<Domain.Tag>>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            response.data = await new Read.Services(db).One(model);

            return response;

        }

        [HttpGet]
        [Route("byTagType")]
        public async Task<DefaultResponse<IList<Domain.Tag>>> ReadByTagType(string tgType)
        {
            object tagType = null;
            if (!Enum.TryParse(typeof(Domain.TagType), tgType, out tagType))
            {
                throw new Exception("erro");
            }

            Tag.Read.Model tg = new Tag.Read.Model();
            tg.TagType = (Domain.TagType) tagType;
            var response = new DefaultResponse<IList<Domain.Tag>>();
            // Retornar enum inválido

            response.data = await new Read.Services(db).ByTagType(tg);

            return response;

        }

    }
}
