using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Enterprise
{
    public class Update
    {
        public class Model
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

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
                                       .Length(5, 100).WithMessage("Email deve conter de 5 a 100 caracteres")
                                       .EmailAddress().WithMessage("Email inválido");

                RuleFor(validate => validate.NewPassword)
                    .Length(8, 20).WithMessage("A nova senha deve conter de 8 a 20 caracteres");

                RuleFor(validate => validate.CurrentPassword)
                    .Length(8, 20).WithMessage("A senha deve conter de 8 a 20 caracteres"); ;

                RuleFor(validate => validate.Name)
                    .Length(3, 50).WithMessage("Nome deve conter de 3 a 50 caracteres")
                    .NotEmpty().WithMessage("O nome não pode ser nulo");

                RuleFor(validate => validate.PhoneNumber)
                   .MaximumLength(14).WithMessage("Limite caracteres ultrapassado")
                   .NotEmpty().WithMessage("O telefone não pode ser nulo");

                RuleFor(validate => validate.Location)
                    .Length(5, 100).WithMessage("Localização precisa conter entre 5 e 100 caracteres")
                    .NotEmpty().WithMessage("A localização não pode ser vazia");

                RuleFor(validate => validate.State)
                    .NotEmpty().WithMessage("O estado não pode ser vazio")
                    .MaximumLength(2).WithMessage("Limite de caracteres ultrapassado");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<Domain.Enterprise> Save(Model model)
            {
                var enterprise = await db.Enterprises.FindAsync(model.Id);

                if (enterprise == null) throw new HttpException(400, "Identificador de empresa inválido");


                if (model.Name != null)
                    enterprise.Name = model.Name;
                

                if (model.Location != null)
                    enterprise.Location = model.Location;


                if (model.State != null)
                    enterprise.State = model.State;


                if (model.PhoneNumber != null)
                    enterprise.PhoneNumber = model.PhoneNumber;


                if (model.Picture != null)
                    enterprise.Picture = model.Picture;


                if (model.Email != null)
                    enterprise.Email = model.Email;


                if (model.CurrentPassword != null && model.NewPassword != null)

                    if (!enterprise.IsPasswordEqualsTo(model.CurrentPassword))
                        throw new HttpException(400, "Senha inválida");
                    else
                        enterprise.SetPassword(model.NewPassword);

                enterprise.UpdatedAt = DateTime.Now;

                await db.SaveChangesAsync();

                return enterprise;
            }

        }


    }


}
