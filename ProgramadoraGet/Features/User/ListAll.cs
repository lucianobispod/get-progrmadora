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
    public class ListAll
    {

        public class Request : IRequest<IList<Response>>
        {
        }

        public class Response : Domain.User
        {
        }

        public class Handler : AsyncRequestHandler<Request, IList<Response>>
        {
            private Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected async override Task<IList<Response>> HandleCore(Request request)
            {
                return await db.Users
                     .Where(w => w.DeletedAt == null)
                     .Include(include => include.Skills)
                     .ThenInclude(include => include.Tag)
                     .Select(user => new Response
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
