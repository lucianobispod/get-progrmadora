using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Enterprise
{
    public class Delete
    {
        public class Model
        {
            public Guid Id { get; set; }
        }

        public class Services
        {
            private readonly Db db;

            public Services (Db db)
            {
                this.db = db;
            }

            public async Task<DateTime?> Trash(Model model)
            {
                var enterprise = await db.Enterprises.FindAsync(model.Id);

                if (enterprise == null) throw new UnauthorizedException();

                enterprise.DeletedAt = DateTime.Now;

                await db.SaveChangesAsync();

                return enterprise.DeletedAt;

            }


        }


    }
}
