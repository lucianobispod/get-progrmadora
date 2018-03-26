using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;

namespace ProgramadoraGet.Features.Match
{
    [Route("api/[controller]")]
    public class MatchController : Controller
    {
        private Db db;

        public MatchController(Db db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost]
        public async Task<DefaultResponse<Domain.Match>> Create ([FromBody] Create.Model model)
        {
            model.EnterpriseId = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            var response = new DefaultResponse<Domain.Match>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            var services = new Create.Services(db);

            response.data = await services.Save(model);

            return response;
        }
        
        [Authorize]
        [HttpDelete]
        public async Task Delete(Delete.Model model)
        {
            model.EnterpriseId = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);

            await new Delete.Services(db).Trash(model);
        }
        
    }
}