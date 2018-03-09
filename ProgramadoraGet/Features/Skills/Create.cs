using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Skills
{
    public class Create
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

            public async Task<Domain.Skills> Save(Model model)
            {
                var user = await db.Users.FindAsync(model.UserId);

                if (user == null) throw new Exception();
                if (user.DeletedAt != null) throw new Exception();

                var tag = await db.Tags.FindAsync(model.TagId);

                if (tag == null) throw new Exception();
                if (tag.DeletedAt != null) throw new Exception();
                if (tag.TagType == Domain.TagType.Normal) throw new Exception();

                var skills = new Domain.Skills
                {
                    UserId = user.Id,
                    TagId = tag.Id
                };

                db.Skills.Add(skills);

                await db.SaveChangesAsync();

                return skills;

            }
        }

    }
}
