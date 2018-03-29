using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Comment
{
    public class Delete
    {
        public class Model
        {
            public Guid MeIdentifier { get; set; }

            public Guid CommentId { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.CommentId).NotEmpty().WithMessage("Identificador de Comentário vazio");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<DateTime?> Trash(Model model)
            {
                var user = await db.Users.FindAsync(model.MeIdentifier);

                if (user == null) throw new NotFoundException();

                if (user != null && user.DeletedAt != null) throw new NotFoundException();

                var comment = await db.Comments.FindAsync(model.CommentId);

                if (comment == null) throw new HttpException(400, "Identificador de Comentário inválido");

                if (comment != null && comment.DeletedAt != null)
                    return comment.DeletedAt;

                if (comment.UserId != user.Id) throw new ForbiddenException();


                comment.DeletedAt = DateTime.Now;

                await db.SaveChangesAsync();

                return comment.DeletedAt;
            }
        }
    }
}
