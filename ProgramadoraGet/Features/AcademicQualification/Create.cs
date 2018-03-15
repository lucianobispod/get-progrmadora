using FluentValidation;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.HistoricAcademic
{
    public class Create
    {
        public class Model
        {
            public string Course { get; set; }

            public string Institution { get; set; }

            public DateTime FinishedAt { get; set; }

            public DateTime StartedAt { get; set; }

            public string Period { get; set; }

            public DateTime CreatedAt { get; set; }

            public DateTime UpdatedAt { get; set; }

            public Guid UserId { get; set; }
        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(r => r.Course)
                   .NotEmpty().WithMessage("Curso não pode ser vazio")
                   .MaximumLength(100).WithMessage("Limite de caracteres ultrapassado");
                RuleFor(r => r.Institution)
                   .NotEmpty().WithMessage("Instituição não pode ser vazia")
                   .MaximumLength(100).WithMessage("Limite de caracteres ultrapassado");
                RuleFor(r => r.FinishedAt)
                   .NotEmpty().WithMessage("Data de término não pode ser vazia");
                RuleFor(r => r.StartedAt)
                   .NotEmpty().WithMessage("Data de início não pode ser vazia");
                RuleFor(r => r.Period)
                   .NotEmpty().WithMessage("Período não pode ser vazio")
                   .MaximumLength(100).WithMessage("Limite de caracteres ultrapassado");
                RuleFor(r => r.UserId)
                   .NotEmpty().WithMessage("O id não pode ser vazio");

            }
            
        }

        public class Services
        {
            private Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            //public async Task<Domain.AcademicQualification> Save (Model model)
            //{
                
            //}
        }

        

    }
}
