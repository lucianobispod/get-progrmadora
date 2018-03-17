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

                RuleFor(r => r.UserId).NotEmpty().WithMessage("Identificador do usuário inválido");
            }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            //public async Task<Domain.Question> Save(Model model)
            //{
            //    var user = await db.Users.FindAsync(model.UserId);

            //    if (user == null) throw new Exception();
            //    if (user != null && user.DeletedAt != null) throw new Exception();

            //    db.Add(new Domain.Question
            //    {
            //        Title = model.Title,
            //        Content = model.Content,
            //        UserId = model.UserId,
                     
            //    });

            //}
        }

    }
}
