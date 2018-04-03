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

                if (user == null) throw new UnauthorizedException();
                if (user.DeletedAt != null) throw new NotFoundException();

                var tag = await db.Tags.FindAsync(model.TagId);

                if (tag == null) throw new HttpException(400, "Identificador vazio");
                if (tag.DeletedAt != null) throw new NotFoundException();
                if (tag.TagType == Domain.TagType.Normal) throw new HttpException(400, "Tag inválida");

                var skills = new Domain.Skills
                {
                    UserId = user.Id,
                    TagId = tag.Id
                };

                db.Skills.Add(skills);

                user.Points += 10;

                await db.SaveChangesAsync();

                return new Domain.Skills{ UserId = skills.UserId, TagId = skills.TagId };

            }
        }

    }
}
