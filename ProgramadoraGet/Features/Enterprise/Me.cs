using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Enterprise
{
    public class Me
    {
        public class Model
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.Id).NotEmpty().WithMessage("Identificador inválido");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<Domain.Enterprise> Me(Model model)
            {
                var enterprise = await db.Enterprises.FindAsync(model.Id);

                if (enterprise == null) throw new Exception();

                if (enterprise != null && enterprise.DeletedAt != null) throw new Exception();

                return enterprise;
            }
        }
    }
}
