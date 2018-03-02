using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }

        public Guid QuestionId { get; set; }


        #region Navigation

        public virtual User User { get; set; }

        public virtual Question Question { get; set; }

        #endregion
    }
}
