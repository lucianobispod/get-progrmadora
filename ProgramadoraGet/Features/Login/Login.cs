using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Login
{
    public class Login
    {
        public class Model
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }
        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.Email)
                    .Length(20, 100).WithMessage("Email deve conter no mínimo entre 20 e 100 caracteres")
                    .EmailAddress().WithMessage("Email inválido");

                RuleFor(validate => validate.Password)
                    .Length(8, 20).WithMessage("A senha deve conter entre 8 e 20 caracteres")
                    .NotEmpty().WithMessage("A senha não pode ser vazia");

            }
        }

        public class TokenInformation
        {
            public string Token { get; set; }

            public DateTime Expiration { get; set; }

        }

        public class Services
        {
            private readonly IConfiguration configuration;

            private Guid Identificador { get; set; }

            private readonly Db db;

            public Services(Db db, IConfiguration configuration)
            {
                this.db = db;
                this.configuration = configuration;
            }

            public async Task<TokenInformation> SingIn(Model model)
            {
                var user = await db.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

                var enterprise = await db.Enterprises.SingleOrDefaultAsync(u => u.Email == model.Email);


                if (enterprise == null && user == null) throw new NotFoundException();
                if (enterprise != null && user != null) throw new ForbiddenException();

                if (user != null && !user.IsPasswordEqualsTo(model.Password)) throw new HttpException(400, "Senha inválida ");

                if (enterprise != null && !enterprise.IsPasswordEqualsTo(model.Password)) throw new HttpException(400, "Senha inválida ");


                if (enterprise != null)
                    Identificador = enterprise.Id;
                

                if (user != null)
                    Identificador = user.Id;


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
               {
                    new Claim(ClaimTypes.PrimarySid, Identificador.ToString()),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: configuration["Token:Issuer"],
                    audience: configuration["Token:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);


                return new TokenInformation { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo};



            }
        }
    }
}
