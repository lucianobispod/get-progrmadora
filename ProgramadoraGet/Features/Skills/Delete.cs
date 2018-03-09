using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Skills
{
    public class Delete
    {
        public class Model
        {
            public Guid UserId { get; set; }

            public Guid TagId { get; set; }
        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.TagId)
                   .NotEmpty().WithMessage("Identificador de tag vazio");

                RuleFor(r => r.UserId)
                    .NotEmpty().WithMessage("Identificador de usuário vazio");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task Trash(Model model)
            {

              var skills = await db.Skills.SingleOrDefaultAsync(s =>
                    s.UserId == model.UserId
                 && s.TagId == model.TagId);

                if (skills == null) throw new Exception();

                 db.Skills.Remove(skills);

                await db.SaveChangesAsync();

            }
        }
    }
}
