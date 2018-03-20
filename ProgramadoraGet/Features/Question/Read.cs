using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Question
{
    public class Read
    {
        public class Model
        {
            public Guid Id { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.Id).NotEmpty().WithMessage("Identificador não pode ser vazio");
            }

        }

        public class QuestionDefault
        {
            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }

            public User QuestionEditor { get; set; }

            public Tag[] Tags { get; set; }

            public Comment[] Comments { get; set; }

            public DateTime CreatedAt { get; set; }

            public DateTime UpdatedAt { get; set; }



            public class Tag
            {
                public Guid Id { get; set; }

                public string Name { get; set; }

                public Domain.TagType Type { get; set; }
            }

            public class Comment
            {

                public Guid Id { get; set; }

                public string CommentText { get; set; }

                public User CommentEditor { get; set; }

                public DateTime CreatedAt { get; set; }

                public DateTime UpdatedAt { get; set; }

            }

            public class User
            {
                public Guid Id { get; set; }

                public string Name { get; set; }

                public string LastName { get; set; }

                public string Description { get; set; }

                public string State { get; set; }

                public string Location { get; set; }

            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }




            public async Task<IList<QuestionDefault>> AllQuestions()
            {
                return await db.Questions
                     .Where(w => w.DeletedAt == null)
                     .Include(include => include.User)
                     .Where(w => w.User.DeletedAt == null)
                     .Include(include => include.QuestionTag)
                     .ThenInclude(include => include.Tag)
                     .Include(include => include.Comment)
                     .Select(question => new QuestionDefault
                     {
                         Id = question.Id,
                         Title = question.Title,
                         Content = question.Content,
                         CreatedAt = question.CreatedAt,
                         UpdatedAt = question.UpdatedAt,
                         QuestionEditor = new QuestionDefault.User
                         {
                             Id = question.User.Id,
                             Name = question.User.Name,
                             LastName = question.User.LastName,
                             Description = question.User.Description,
                             Location = question.User.Location,
                             State = question.User.State

                         },
                         Tags = question.QuestionTag.Where(w => w.Tag.DeletedAt == null).Select(tag => new QuestionDefault.Tag
                         {
                             Id = tag.Tag.Id,
                             Name = tag.Tag.Name,
                             Type = tag.Tag.TagType
                         }).ToArray(),
                         Comments = question.Comment.Where(w => w.DeletedAt == null).Select(comment => new QuestionDefault.Comment
                         {
                             Id = comment.Id,
                             CommentText = comment.CommentText,
                             CreatedAt = comment.CreatedAt,
                             UpdatedAt = comment.UpdatedAt,
                             CommentEditor = new QuestionDefault.User
                             {
                                 Id = comment.User.Id,
                                 Description = comment.User.Description,
                                 Name = comment.User.Name,
                                 LastName = comment.User.LastName,
                                 Location = comment.User.Location,
                                 State = comment.User.State
                             }
                         }).ToArray()
                     }).ToListAsync();
            }

            public async Task<IList<QuestionDefault>> OneQuestion(Model model)
            {

                await db.Questions.FindAsync(model.Id);

                return await db.Questions
                     .Where(w => w.Id == model.Id)
                     .Where(w => w.DeletedAt == null)
                     .Include(include => include.User)
                     .Where(w => w.User.DeletedAt == null)
                     .Include(include => include.QuestionTag)
                     .ThenInclude(include => include.Tag)
                     .Include(include => include.Comment)
                     .Select(question => new QuestionDefault
                     {
                         Id = question.Id,
                         Title = question.Title,
                         Content = question.Content,
                         CreatedAt = question.CreatedAt,
                         UpdatedAt = question.UpdatedAt,
                         QuestionEditor = new QuestionDefault.User
                         {
                             Id = question.User.Id,
                             Name = question.User.Name,
                             LastName = question.User.LastName,
                             Description = question.User.Description,
                             Location = question.User.Location,
                             State = question.User.State

                         },
                         Tags = question.QuestionTag.Where(w => w.Tag.DeletedAt == null).Select(tag => new QuestionDefault.Tag
                         {
                             Id = tag.Tag.Id,
                             Name = tag.Tag.Name,
                             Type = tag.Tag.TagType
                         }).ToArray(),
                         Comments = question.Comment.Where(w => w.DeletedAt == null).Select(comment => new QuestionDefault.Comment
                         {
                             Id = comment.Id,
                             CommentText = comment.CommentText,
                             CreatedAt = comment.CreatedAt,
                             UpdatedAt = comment.UpdatedAt,
                             CommentEditor = new QuestionDefault.User
                             {
                                 Id = comment.User.Id,
                                 Description = comment.User.Description,
                                 Name = comment.User.Name,
                                 LastName = comment.User.LastName,
                                 Location = comment.User.Location,
                                 State = comment.User.State
                             }
                         }).ToArray()
                     }).ToListAsync();
            }

        }
    }
}
