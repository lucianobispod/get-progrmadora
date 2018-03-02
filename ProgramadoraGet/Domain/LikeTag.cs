using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class LikeTag
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TagId { get; set; }

        #region Navigation

        public User User { get; set; }

        public Tag Täg { get; set; }

        #endregion
    }
}
