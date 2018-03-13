using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IList<Domain.Enterprise>>  ReadAll()
        {
            return await new Read.Services(db).All();
        }


        [HttpGet]
        [Route("{Id}")]
        public async Task<IList<Domain.Enterprise>> ReadOne(Read.Model model)
        {

            return await new Read.Services(db).One(model);
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<DefaultResponse<Domain.Enterprise>> Update([FromBody]Update.Model model)
        {
            model.Id = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);

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
        [Route("{Id}")]
        public async Task<DateTime?> Delete(Delete.Model model)
        {
            return await new Delete.Services(db).Trash(model);
        }

        [Authorize]
        [HttpGet]
        [Route("Me")]
        public async Task<Domain.Enterprise> Me()
        {
            return await new Me.Services(db).Me(new Me.Model { Id = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value) });
        }
    }
}