using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public class Response
        {
            public string Token { get; set; }

            public DateTime Expiration { get; set; }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async void SingIn(Model model)
            {
                var user = await db.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

                if (user != null && !user.IsPasswordEqualsTo(model.Password)) throw new Exception();


                var enterprise = await db.Enterprises.SingleOrDefaultAsync(u => u.Email == model.Email);

                if (enterprise != null && !enterprise.IsPasswordEqualsTo(model.Password)) throw new Exception();




            }
        }
    }
}
