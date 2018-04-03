using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Comment
{
    public class Create
    {
        public class Model
        {
            public string CommentText { get; set; }

            public Guid UserId { get; set; }

            public Guid QuestionId { get; set; }
        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.CommentText)
                    .MaximumLength(200).WithMessage("Limite de caracteres ultrapassado")
                    .NotEmpty().WithMessage("Este campo não pode ser vazio");

                RuleFor(r => r.QuestionId)
                    .NotEmpty().WithMessage("Identificador de Pergunta não pode ser vazio");

            }
        }


        public class DefaultResponse
        {
            public Guid Id { get; set; }

            public string CommentText { get; set; }

            public DateTime CreatedAt { get; set; }

            public Guid QuestionId { get; set; }

        }

        public class Services
        {

            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<DefaultResponse> Save(Model model)
            {
                var user = await db.Users.FindAsync(model.UserId);

                if (user == null) throw new NotFoundException();
                if (user != null && user.DeletedAt != null) throw new NotFoundException();

                var question = await db.Questions.FindAsync(model.QuestionId);

                if (question == null) throw new HttpException(400, "Identificador de Question inválido");
                if (question != null && question.DeletedAt != null) throw new NotFoundException();

                var comment = new Domain.Comment
                {
                    CommentText = model.CommentText,
                    QuestionId = model.QuestionId,
                    UserId = model.UserId,
                };

                db.Comments.Add(comment);

                user.Points += 10;

                await db.SaveChangesAsync();

                return new DefaultResponse {  Id = comment.Id, CommentText = comment.CommentText, CreatedAt = comment.CreatedAt, QuestionId = comment.QuestionId} ;
            }
        }



    }
}

