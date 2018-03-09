using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Technology
{
    public class Create
    {
        public class Model
        {
            public string Name { get; set; }
        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.Name)
                   .NotEmpty().WithMessage("Nome não pode ser vazio")
                   .MaximumLength(100).WithMessage("Limite de caracteres ultrapassado");
            }
        }


        public class Services
        {

            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<Domain.Tag> Save(Model model)
            {
                var technology = db.Tags.SingleOrDefault(s => s.Name.ToLower() == model.Name.ToLower() && s.TagType == Domain.TagType.Technology);

                if (technology != null)
                {
                    if (technology.DeletedAt != null)
                    {
                        technology.DeletedAt = null;
                        await db.SaveChangesAsync();
                    }

                    return technology;
                }

                technology = new Domain.Tag
                {
                    Name = model.Name,
                    TagType = Domain.TagType.Technology
                };

                db.Tags.Add(technology);

                await db.SaveChangesAsync();

                return technology;

            }
        }
    }
}
