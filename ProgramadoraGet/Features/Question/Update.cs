using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Question
{
    public class Update
    {
        public class Model
        {
            public Guid QuestionIdentifier { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }

            public Guid UserIdentifier { get; set; }

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
                var user = await db.Users.FindAsync(model.UserIdentifier);

                if (user == null) throw new Exception();
                if (user != null && user.DeletedAt != null) throw new Exception();

                var question = await db.Questions.FindAsync(model.QuestionIdentifier);

                if (question == null) throw new Exception();
                if (question != null && question.DeletedAt != null) throw new Exception();

                if (question.UserId != model.UserIdentifier) throw new Exception();


                question.Title = model.Title;

                question.Content = model.Content;



                var Tags = new List<QuestionDefault.Tag>();



                var questionTag = await db.QuestionTags
                    .Where(s => s.QuestionId == question.Id)
                    .Select(r => new Domain.QuestionTag
                    {
                        QuestionId = r.QuestionId,
                        TagId = r.TagId
                    }).ToListAsync();


                db.QuestionTags.RemoveRange(questionTag);

                await db.SaveChangesAsync();

                if (model.Tags.Count() > 0)
                {
                    var tagsDisyinct = model.Tags.Distinct();

                    if (tagsDisyinct.Count() > 10) throw new Exception();

                    foreach (var item in tagsDisyinct)
                    {

                        var tag = await db.Tags.FindAsync(item);

                        if (tag == null) throw new Exception();
                        if (tag != null && tag.DeletedAt != null) throw new Exception();


                        var questiontagExists = await db.QuestionTags
                            .SingleOrDefaultAsync(s => s.QuestionId == question.Id && s.TagId == item);

                        if (questiontagExists == null)
                        {
                            db.QuestionTags.Add
                                    (new Domain.QuestionTag { QuestionId = question.Id, TagId = item });
                        }

                        Tags.Add(new QuestionDefault.Tag { Id = tag.Id, Name = tag.Name, Type = tag.TagType });

                    }

                    await db.SaveChangesAsync();
                }

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
