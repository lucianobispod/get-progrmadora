using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Match
{
    public class Read
    {
        public class Model
        {
            public Guid EnterpriseId { get; set; }
        }

        

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<IList<Domain.Match>> One(Model model)
            {
                if (model.EnterpriseId == null) throw new HttpException(400);

                return await db.Matches
                    .Where(w => w.EnterpriseId == model.EnterpriseId)
                    .Include(i => i.User)
                    .Select(match => new Domain.Match
                    {
                        EnterpriseId = match.EnterpriseId,
                        UserId = match.UserId, // TODO: Verificar retorno

                    }).ToListAsync();
            }

        }
        
    }
}
