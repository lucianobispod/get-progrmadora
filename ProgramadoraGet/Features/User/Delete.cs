using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
{
    public class Delete
    {
        public class Model
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.Id).NotEmpty().WithMessage("Identificador nulo");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<DateTime?> Trash(Model model)
            {
                var user = await db.Users.FindAsync(model.Id);
                
                if (user == null) throw new Exception();

                user.DeletedAt = DateTime.Now;

                await db.SaveChangesAsync();
                
                return user.DeletedAt;
            }

        }
    }
}
