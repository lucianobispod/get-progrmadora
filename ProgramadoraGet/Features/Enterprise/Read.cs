using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Enterprise
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

            public async Task<IList<Domain.Enterprise>> All()
            {
                return await db.Enterprises
                     .Where(w => w.DeletedAt == null)
                     .Select(user => new Domain.Enterprise
                     {
                         Id = user.Id,
                         Name = user.Name,
                         Email = user.Email,
                         Location = user.Location,
                         State = user.State,
                         PhoneNumber = user.PhoneNumber,
                         Picture = user.Picture,
                         CreatedAt = user.CreatedAt,
                         UpdatedAt = user.UpdatedAt

                     }).ToListAsync();
            }

            public async Task<IList<Domain.Enterprise>> One(Model model)
            {
                if (model.Id == null) throw new Exception();

                return await db.Enterprises
                     .Where(e => e.DeletedAt == null)
                     .Where(e => e.Id == model.Id)
                     .Select(enterprise => new Domain.Enterprise
                     {

                         Id = enterprise.Id,
                         Name = enterprise.Name,
                         Email = enterprise.Email,
                         Location = enterprise.Location,
                         State = enterprise.State,
                         PhoneNumber = enterprise.PhoneNumber,
                         Picture = enterprise.Picture,
                         CreatedAt = enterprise.CreatedAt,
                         UpdatedAt = enterprise.UpdatedAt

                     }).ToListAsync();
            }


        }


    }
}
