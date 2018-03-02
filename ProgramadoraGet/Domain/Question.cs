using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }

        public Guid UserId { get; set; }

        public Guid QuestionTagId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }


        #region Navigation

        public User User { get; set; }

        public ICollection<QuestionTag> QuestionTag { get; set; }
        #endregion
    }
}
