using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class RecoveryPassword
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public DateTime? AcessedAt { get; set; }

        #region Navigation

        public virtual User User { get; set; }


        #endregion
    }
}
