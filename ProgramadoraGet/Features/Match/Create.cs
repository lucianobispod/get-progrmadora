using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Match
{
    public class Create
    {
        public class Model
        {
            public Guid UserId { get; set; }
            public Guid EnterpriseId { get; set; }
        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.UserId)
                   .NotEmpty().WithMessage("Identificador de usuário vazio");
                RuleFor(r => r.EnterpriseId)
                   .NotEmpty().WithMessage("Identificador de empresa vazio");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<Domain.Match> Save(Model model)
            {
                var user = await db.Users.FindAsync(model.UserId);

                if (user == null) throw new Exception();
                if (user.DeletedAt != null) throw new Exception();

                var enterprise = await db.Enterprises.FindAsync(model.EnterpriseId);

                if (enterprise == null) throw new Exception();
                if (enterprise.DeletedAt != null) throw new Exception();

                var match = new Domain.Match
                {
                    UserId = user.Id,
                    EnterpriseId = enterprise.Id
                };
                
                db.Matches.Add(match);

                await db.SaveChangesAsync();

                return match;
            }
        }



    }
}
