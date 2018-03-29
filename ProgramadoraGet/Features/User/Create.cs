using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
{
    public class Create
    {
        public class Model 
        {
            public string Name { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(validate => validate.Email)
                    .Length(20, 100).WithMessage("Email deve conter de 20 a 100 caracteres")
                    .EmailAddress().WithMessage("Email inválido");

                RuleFor(validate => validate.Password)
                    .Length(8, 20).WithMessage("A senha deve conter entre 8 e 20 caracteres")
                    .NotEmpty().WithMessage("A senha não pode ser vazia");

                RuleFor(validate => validate.LastName)
                    .NotEmpty().WithMessage("O Sobrenome não pode ser vazio")
                    .MaximumLength(50).WithMessage("Limite de caracter ultrapassado");

                RuleFor(validate => validate.Name)
                    .Length(3, 50).WithMessage("Campo nome precisa conter entre 3 e 50 caracteres")
                    .NotEmpty().WithMessage("O Nome não pode ser vazio");
            }
        }

        public class Services
        {

            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }


            public async Task<Domain.User> Save(Model model)
            {

                if (await db.Users.SingleOrDefaultAsync(s => s.Email == model.Email) != null) throw new HttpException(400, "Email já cadastrado");

                var user = new Domain.User
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Email = model.Email
                };

                user.SetPassword(model.Password);

                db.Users.Add(user);

                await db.SaveChangesAsync();

                return new Domain.User { Id = user.Id, Name = user.Name, LastName = user.LastName, Email = user.Email };

            }

        }


    }
}
