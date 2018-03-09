using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;

namespace ProgramadoraGet.Features.Enterprise
{
    [Route("api/[controller]")]
    public class EnterpriseController : Controller
    {
        private Db db;
        
        public EnterpriseController (Db db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<DefaultResponse<Domain.Enterprise>> Create ([FromBody] Create.Model model)
        {
            var response = new DefaultResponse<Domain.Enterprise>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            var services = new Create.Services(db);

            response.data = await services.Save(model);

            return response;
        }
        
        [HttpGet]
        public async Task<DefaultResponse<IList<Domain.Enterprise>>>  ReadAll()
        {
            var response = new DefaultResponse<IList<Domain.Enterprise>>();

            response.data = await new Read.Services(db).All();

            return response;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<DefaultResponse<IList<Domain.Enterprise>>> ReadOne(Read.Model model)
        {
            var response = new DefaultResponse<IList<Domain.Enterprise>> ();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }
            
            response.data = await new Read.Services(db).One(model);

            return response;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<DefaultResponse<Domain.Enterprise>> Update(Guid id, [FromBody]Update.Model model)
        {
            model.Id = id;

            var response = new DefaultResponse<Domain.Enterprise>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            response.data = await new Update.Services(db).Save(model);

            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<DefaultResponse<DateTime?>> Delete(Delete.Model model)
        {
            var response = new DefaultResponse<DateTime?>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            response.data = await new Delete.Services(db).Trash(model);

            return response;
        }


    }
}