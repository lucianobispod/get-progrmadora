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

        public class Model
        {
            public Guid Identificador { get; set; }

        }

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
                     .OrderByDescending(w => w.Points)
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
                         Points = user.Points,
                         Skills = user.Skills.Where(w => w.Tag.DeletedAt == null).Select(skills => new Domain.Skills
                         {
                             Tag = new Domain.Tag
                             {
                                 Id = skills.Tag.Id,
                                 Name = skills.Tag.Name,
                             }
                         }).ToList()

                     }).ToListAsync();
            }

            public async Task<IList<Domain.User>> OnlySkill(Model model)
            {
                if (model.Identificador == null) throw new NotFoundException();

                return await db.Skills
                     .Include(include => include.User)
                     .Where(w => w.TagId == model.Identificador)
                     .Select(skills => new Domain.User
                     {
                         Id = skills.User.Id,
                         Name = skills.User.Name,
                         LastName = skills.User.LastName,
                         Description = skills.User.Description,
                         Email = skills.User.Email,
                         Location = skills.User.Location,
                         State = skills.User.State,
                         PhoneNumber = skills.User.PhoneNumber,
                         Picture = skills.User.Picture,
                         Points = skills.User.Points,
                         CreatedAt = skills.User.CreatedAt,
                         UpdatedAt = skills.User.UpdatedAt,
                     }).ToListAsync();
            }

            public async Task<IList<Domain.User>> One(Model model)
            {
                if (model.Identificador == null) throw new NotFoundException();

                return await db.Users
                     .Where(w => w.DeletedAt == null)
                     .Where(w => w.Id == model.Identificador)
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
                          Points = user.Points,
                         CreatedAt = user.CreatedAt,
                         UpdatedAt = user.UpdatedAt,
                         Skills = user.Skills.Where(w => w.Tag.DeletedAt == null).Select(skills => new Domain.Skills
                         {
                             Tag = new Domain.Tag
                             {
                                 Id = skills.Tag.Id,
                                 Name = skills.Tag.Name,
                                 TagType = skills.Tag.TagType
                             }
                         }).ToList()

                     }).ToListAsync();
            }

        }

    }
}
