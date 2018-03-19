using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Tag
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
                var tag = db.Tags.SingleOrDefault(s => s.Name.ToLower() == model.Name.ToLower() && s.TagType == Domain.TagType.Normal);

                if (tag != null)
                {
                    if (tag.DeletedAt != null)
                    {
                        tag.DeletedAt = null;
                        await db.SaveChangesAsync();
                    }

                    return tag;
                }

                tag = new Domain.Tag
                {
                    Name = model.Name,
                    TagType = Domain.TagType.Normal
                };

                db.Tags.Add(tag);

                await db.SaveChangesAsync();

                return new Domain.Tag { Id = tag.Id , Name = tag.Name, TagType = tag.TagType };

            }
        }
    }
}
