using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.User
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private Db db;

        public UserController(Db db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<DefaultResponse<Domain.User>> Create([FromBody] Create.Model model)
        {
            var response = new DefaultResponse<Domain.User>();

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
        public async Task<IList<Domain.User>> ReadAll()
        {
            return await new Read.Services(db).All();
        }
    }
}
