using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class QuestionTag
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public Guid TagId { get; set; }

        #region Navigation

        public Question Question { get; set; }

        public Tag Tag { get; set; }
        #endregion

    }
}
