using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Comment
{
    public class Update
    {
        public class Model
        {
            public Guid CommentId { get; set; }

            public string CommentText { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(validate => validate.CommentText)
                     .MaximumLength(200).WithMessage("Email deve conter de 20 a 100 caracteres")
                     .NotEmpty().WithMessage("Commentário não pode ser vazio");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }


            public async Task<Domain.Comment> Save(Model model)
            {
                var comment = await db.Comments.FindAsync(model.CommentId);

                if (comment == null) throw new Exception();

                if (comment.DeletedAt != null) throw new Exception();

                comment.CommentText = model.CommentText;
                comment.UpdatedAt = DateTime.Now;

                await db.SaveChangesAsync();

                return comment;
            }
        }
    }
}
