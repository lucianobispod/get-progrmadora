using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;

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
        [Route("byTagType/{TagType}")]
        public async Task<DefaultResponse<IList<Domain.Tag>>> ReadByTagType(Read.Model model)
        {
            var response = new DefaultResponse<IList<Domain.Tag>>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            response.data = await new Read.Services(db).ByTagType(model);

            return response;

        }

        //[HttpGet]
        //[Route("byTagType/normal")]
        //public async Task<DefaultResponse<IList<Domain.Tag>>> ReadByTagTypeNormal(Read.Model model)
        //{

        //    return ReadByTagType(new Read.Model { TagType = 0 });


        //}


    }
}
