using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.User
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<Create.Response> Create([FromBody] Create.Command request)
        {
           return await mediator.Send(request);
        }

        [HttpGet]
        public async Task<IList<ListAll.Response>> ListAll(ListAll.Request request)
        {
            return await mediator.Send(request);
        }
    }
}
