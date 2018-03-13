using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class HistoricAcademic
    {
        public Guid Id { get; set; }

        public string Course { get; set; }

        public string Institution { get; set; }

        public DateTime FinishedAt { get; set; }

        public DateTime StartedAt { get; set; }

        public string Period { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid UserId { get; set; }

        #region Navigation

        public virtual User User { get; set; }

        #endregion
    }
}
