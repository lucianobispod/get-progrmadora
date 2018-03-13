using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Enterprise
{
    public class Create
    {
        public class Model
        {
            public string Name { get; set; }

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
                
                RuleFor(validate => validate.Name)
                    .Length(3, 50).WithMessage("mail deve conter de 3 a 50 caracteres")
                    .NotEmpty().WithMessage("O nome não pode ser vazio");
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

                if (await db.Enterprises.SingleOrDefaultAsync(s => s.Email == model.Email) != null) throw new Exception();

                var enterprise = new Domain.Enterprise
                {
                    Name = model.Name,
                    Email = model.Email
                };

                enterprise.SetPassword(model.Password);

                db.Enterprises.Add(enterprise);

                await db.SaveChangesAsync();

                return new Domain.Enterprise { Id = enterprise.Id, Name = enterprise.Name, Email = enterprise.Email };

            }
        }
        
    }
}
