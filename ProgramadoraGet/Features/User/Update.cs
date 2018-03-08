using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
{
    public class Update
    {

        public class Model
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string LastName { get; set; }

            public string Description { get; set; }

            public string Email { get; set; }

            public string CurrentPassword { get; set; }

            public string NewPassword { get; set; }

            public string State { get; set; }

            public string Location { get; set; }

            public string PhoneNumber { get; set; }

            public string Picture { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(validate => validate.Email)
                       .Length(20, 100).WithMessage("Email deve conter de 20 a 100 caracteres")
                       .EmailAddress().WithMessage("Email inválido");

                RuleFor(validate => validate.NewPassword)
                    .Length(8, 20);

                RuleFor(validate => validate.CurrentPassword)
                    .Length(8, 20);

                RuleFor(validate => validate.LastName)
                    .NotEmpty()
                    .Length(5, 50);

                RuleFor(validate => validate.Description)
                    .MaximumLength(100);

                RuleFor(validate => validate.Name)
                    .Length(3, 50)
                    .NotEmpty();

                RuleFor(validate => validate.PhoneNumber)
                   .MaximumLength(14).WithMessage("limite caracteres ultrapassado")
                   .NotEmpty().WithMessage("O Telefone não pode ser nulo");

                RuleFor(validate => validate.Location)
                    .Length(5, 100).WithMessage("Localização precisa conter entre 5 e 100 caracteres")
                    .NotEmpty().WithMessage("A senha não pode ser vazia");

                RuleFor(validate => validate.State)
                    .NotEmpty().WithMessage("O Estado não pode ser vazio")
                    .MaximumLength(2).WithMessage("Limite de caracter ultrapassado");

                RuleFor(validate => validate.Description)
                    .MaximumLength(100).WithMessage("limite de caracteres ultrapassados");
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
                var user = await db.Users.FindAsync(model.Id);

                if (user == null) throw new Exception();


                if (model.Name != null)
                    user.Name = model.Name;


                if (model.LastName != null)
                    user.LastName = model.LastName;


                if (model.Description != null)
                    user.Description = model.Description;


                if (model.Location != null)
                    user.Location = model.Location;


                if (model.State != null)
                    user.State = model.State;


                if (model.PhoneNumber != null)
                    user.PhoneNumber = model.PhoneNumber;


                if (model.Picture != null)
                    user.Picture = model.Picture;


                if (model.Email != null)
                    user.Email = model.Email;


                if (model.CurrentPassword != null && model.NewPassword != null)

                    if (!user.IsPasswordEqualsTo(model.CurrentPassword)) throw new Exception();
                    else user.SetPassword(model.NewPassword);


                await db.SaveChangesAsync();

                return user;
            }
        }
    }
}
