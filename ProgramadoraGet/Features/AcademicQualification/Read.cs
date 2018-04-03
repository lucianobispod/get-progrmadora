using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.AcademicQualification
{
    public class Read
    {
        public class Model
        {
            public Guid Id { get; set; }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<IList<Domain.AcademicQualification>> One(Model model)
            {
                if (model.Id == null) throw new NotFoundException();

                return await db.AcademicQualifications
                    .Where(w => w.Id == model.Id)
                    .Select(aq => new Domain.AcademicQualification
                    {
                        Id = aq.Id,
                        Course = aq.Course,
                        Institution = aq.Institution,
                        FinishedAt = aq.FinishedAt,
                        StartedAt = aq.StartedAt,
                        Period = aq.Period,
                        CreatedAt = aq.CreatedAt,
                        UpdatedAt = aq.UpdatedAt,
                        UserId = aq.UserId

                    }).ToListAsync();
            }
            
        }

    }
}
