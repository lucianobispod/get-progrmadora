using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Login
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;

        private readonly Db db;

        public LoginController(IConfiguration configuration, Db db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<Login.TokenInformation> Login([FromBody] Login.Model model)
        {
            return await new Login.Services(db, configuration).SingIn(model);
        }


    }
}
