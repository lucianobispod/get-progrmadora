using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Feedback
{
    public class Create
    {
        public class Model 
        {
            public Guid UserId { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }
        }


        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(validate => validate.Title)
                    .NotEmpty().WithMessage("Titulo não pode ser vazio")
                    .MaximumLength(100).WithMessage("Ultrapassado o limite de caracteres");

                RuleFor(validate => validate.Content)
                    .MaximumLength(200).WithMessage("Ultrapassado o limite de caracteres")
                    .NotEmpty().WithMessage("Conteúdo não pode ser vazio");
            }
        }
        
        public class Services 
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<Domain.Feedback> Save(Model model)
            {
                var feedback = new Domain.Feedback()
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = model.UserId
                };

                db.Feedbacks.Add(feedback);

                await db.SaveChangesAsync();

                return new Domain.Feedback { Id = feedback.Id, Title = feedback.Title, Content = feedback.Content, UserId = feedback.UserId };

            }
        }
    }
}
