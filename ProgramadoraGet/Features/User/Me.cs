using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
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
                RuleFor(r => r.Id).NotEmpty().WithMessage("Token inválido");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<Domain.User> Me(Model model)
            {
                var user = await db.Users.FindAsync(model.Id);

                if (user == null) throw new UnauthorizedException();

                if (user != null && user.DeletedAt != null) throw new NotFoundException();

                return user;
            }
        }
    }
}
