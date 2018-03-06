using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
{
    public class Create
    {
        public class Command : IRequest<Response>
        {
            public string Name { get; set; }

            public string LastName { get; set; }

            public string Description { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string State { get; set; }

            public string Location { get; set; }
        }


        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(validate => validate.Email).Length(20, 100).EmailAddress();
                RuleFor(validate => validate.Password).Length(8, 20);
                RuleFor(validate => validate.LastName).NotEmpty().Length(5, 50);
                RuleFor(validate => validate.Description).MaximumLength(100);
                RuleFor(validate => validate.Name).Length(3, 50).NotEmpty();
            }
        }

        public class Response : Domain.User
        {
        }

        public class Handler : AsyncRequestHandler<Command, Response>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<Response> HandleCore(Command request)
            {
                if (await db.Users.SingleOrDefaultAsync(s => s.Email == request.Email) != null) throw new Exception();

                var user = new Domain.User
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Description = request.Description,
                    Email = request.Email,
                    State = request.State,
                    Location = request.Location

                };

                user.SetPassword(request.Password);

                db.Users.Add(user);

                await db.SaveChangesAsync();

                return new Response
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Description = user.Description,
                    Email = user.Email,
                    Location = user.Location,
                    State = user.State,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };
            }
        }


    }
}
