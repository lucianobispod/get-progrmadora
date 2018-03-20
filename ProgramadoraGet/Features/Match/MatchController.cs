using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;

namespace ProgramadoraGet.Features.Match
{
    [Route("api/[controller]")]
    public class MatchController : Controller
    {
        //TODO: Delete

        private Db db;

        public MatchController(Db db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<DefaultResponse<Domain.Match>> Create ([FromBody] Create.Model model)
        {
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

        [HttpGet("{EnterpriseId}")]
        public async Task<IList<Domain.Match>> ReadOne (Read.Model model)
        {
            return await new Read.Services(db).One(model);
        }

    }
}