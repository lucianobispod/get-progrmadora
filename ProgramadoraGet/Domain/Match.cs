using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class Match
    {
        public Guid UserId { get; set; }

        public Guid EnterpriseId { get; set; }

        #region Navigation

        public virtual User User { get; set; }

        public virtual Enterprise Enterprise { get; set; }

        #endregion
    }
}
