using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Match
{
    public class Delete
    {
        public class Model
        {
            public Guid EnterpriseId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Services
        {
            private Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task Trash(Model model)
            {
                if (model.EnterpriseId == null) throw new Exception("Id da empresa é nulo");
                if (model.UserId == null) throw new Exception("Id do usuário é nulo");

                var match = await db.Matches.SingleOrDefaultAsync(s =>
                    s.UserId == model.UserId
                 && s.EnterpriseId == model.EnterpriseId);

                if (match == null) throw new Exception();

                db.Matches.Remove(match);

                await db.SaveChangesAsync();
            }

        }


    }
}
