using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<Domain.User> Create([FromBody] Create.Model model)
        {
            return await new Create.Services(db).Save(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IList<Domain.User>> ReadAll()
        {
            return await new Read.Services(db).All();
        }

        [Authorize]
        [HttpGet]
        [Route("{Id}")]
        public async Task<IList<Domain.User>> ReadOne(Read.Model model)
        {
            return await new Read.Services(db).One(model);
        }

        [Authorize]
        [HttpDelete]
        public async Task<DateTime?> Delete()
        {
            return await new Delete.Services(db).Trash(new Delete.Model { Id = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value) });
        }

        [Authorize]
        [HttpGet]
        [Route("Me")]
        public async Task<Domain.User> Me()
        {
            return await new Me.Services(db).Me(new Me.Model { Id = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value) });
        }

        [Authorize]
        [HttpPut]
        public async Task<Domain.User> Update([FromBody] Update.Model model)
        {
            model.Id = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
            return await new Update.Services(db).Save(model);
        }
    }
}
