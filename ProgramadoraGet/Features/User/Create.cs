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

            public string Description { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string State { get; set; }

            public string Location { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(validate => validate.Email)
                    .Length(20, 100).WithMessage("Email deve conter de 20 a 100 caracteres")
                    .EmailAddress().WithMessage("Email inválido");

                RuleFor(validate => validate.Password)
                    .Length(8, 20);

                RuleFor(validate => validate.LastName)
                    .NotEmpty()
                    .Length(5, 50);

                RuleFor(validate => validate.Description)
                    .MaximumLength(100);

                RuleFor(validate => validate.Name)
                    .Length(3, 50)
                    .NotEmpty();
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

                if (await db.Users.SingleOrDefaultAsync(s => s.Email == model.Email) != null) throw new Exception();

                var user = new Domain.User
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Description = model.Description,
                    Email = model.Email,
                    State = model.State,
                    Location = model.Location,
                };

                user.SetPassword(model.Password);

                db.Users.Add(user);

                await db.SaveChangesAsync();

                return user;

            }

        }


    }
}
