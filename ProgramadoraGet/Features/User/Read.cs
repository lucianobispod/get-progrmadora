using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Domain;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
{
    public class Read
    {

        public class Services 
        {
            private Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<IList<Domain.User>> All()
            {
                return await db.Users
                     .Where(w => w.DeletedAt == null)
                     .Include(include => include.Skills)
                     .ThenInclude(include => include.Tag)
                     .Select(user => new Domain.User
                     {
                         Id = user.Id,
                         Name = user.Name,
                         LastName = user.LastName,
                         Description = user.Description,
                         Email = user.Email,
                         Location = user.Location,
                         State = user.State,
                         PhoneNumber = user.PhoneNumber,
                         Picture = user.Picture,
                         CreatedAt = user.CreatedAt,
                         UpdatedAt = user.UpdatedAt,
                         Skills = user.Skills.Where(w => w.Tag.DeletedAt == null).Select(skills => new Skills
                         {
                             Tag = new Tag
                             {
                                 Id = skills.Tag.Id,
                                 Name = skills.Tag.Name,
                             }
                         }).ToList()

                     }).ToListAsync();
            }
        }

    }
}
