using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Feedback
{
    public class Read
    {

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }


            public async Task<IList<Domain.Feedback>> All()
            {
                return await db.Feedbacks
                    .Where(w => w.DeletedAt == null)
                    .Select(feedback => new Domain.Feedback
                    {
                        Id = feedback.Id,
                        Title = feedback.Title,
                        Content = feedback.Content,
                        CreatedAt = feedback.CreatedAt,
                        UserId = feedback.UserId
                    }).ToListAsync();
            }

        }
    }
}
