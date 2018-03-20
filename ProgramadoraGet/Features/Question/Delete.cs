using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Question
{
    public class Delete
    {
        public class Model
        {
            public Guid QuestionId { get; set; }

            public Guid MeIdentifier { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.QuestionId).NotEmpty().WithMessage("Identificador vazio");

                RuleFor(r => r.MeIdentifier).NotEmpty().WithMessage("Identificador de usuário vazio");
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
                var question = await db.Questions.FindAsync(model.QuestionId);

                var user = await db.Users.FindAsync(model.MeIdentifier);

                if (user == null) throw new Exception();

                if (question == null) throw new Exception();

                if (question != null && question.DeletedAt != null)
                    return question.DeletedAt;

                if (question.UserId != user.Id) throw new Exception("Você não pode apagar uma question que não é sua, pô se liga!");


                question.DeletedAt = DateTime.Now;

                await db.SaveChangesAsync();

                return question.DeletedAt;
            }
        }
    }
}
