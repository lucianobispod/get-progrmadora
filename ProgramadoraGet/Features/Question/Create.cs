using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Question
{
    public class Create
    {
        public class Model
        {
            public string Content { get; set; }

            public string Title { get; set; }

            public Guid UserId { get; set; }

            public Guid[] Tags { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.Content)
                    .NotEmpty().WithMessage("Conteúdo não pode ser vazio")
                    .MaximumLength(500).WithMessage("limite de caracteres ultrapassado");

                RuleFor(r => r.Title)
                    .NotEmpty().WithMessage("titulo não pode ser vazio")
                    .MaximumLength(150).WithMessage("limite de caracteres ultrapassado");

            }
        }


        public class QuestionDefault
        {
            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }

            public DateTime CreatedAt { get; set; }

            public DateTime UpdatedAt { get; set; }

            public List<Tag> Tags { get; set; }

            public Guid UserId { get; set; }

            public class Tag
            {
                public Guid Id { get; set; }

                public string Name { get; set; }

                public Domain.TagType Type { get; set; }
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<QuestionDefault> Save(Model model)
            {
                var user = await db.Users.FindAsync(model.UserId);

                if (user == null) throw new UnauthorizedException();
                if (user != null && user.DeletedAt != null) throw new NotFoundException();

                user.Points += 10;

                var question = new Domain.Question
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = model.UserId,
                };

                db.Questions.Add(question);


                var questionTags = new List<Domain.QuestionTag>();
                var Tags = new List<QuestionDefault.Tag>();

                var tagsDisyinct = model.Tags
                      .Distinct();

                if (tagsDisyinct.Count() > 10) throw new HttpException(400, "Limite de tag ultrapassado");

                foreach (var item in tagsDisyinct)
                {

                    var tag = await db.Tags.FindAsync(item);

                    if (tag == null) throw new Exception();

                    Tags.Add(new QuestionDefault.Tag { Id = tag.Id, Name = tag.Name, Type = tag.TagType });

                    questionTags.Add(new Domain.QuestionTag { TagId = item, QuestionId = question.Id });


                }

                db.QuestionTags.AddRange(questionTags);

                await db.SaveChangesAsync();

                return new QuestionDefault
                {
                    Id = question.Id,
                    Title = question.Title,
                    Content = question.Content,
                    CreatedAt = question.CreatedAt,
                    UpdatedAt = question.UpdatedAt,
                    UserId = question.UserId,
                    Tags = Tags
                };
            }
        }

    }
}


