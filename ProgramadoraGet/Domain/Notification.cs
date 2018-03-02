using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class Notification
    {
        public Guid Id { get; set; }

        public String Title { get; set; }

        public String Link { get; set; }

        public Guid UserId { get; set; }

        public DateTime? ViewAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        #region Navigation

        public User User { get; set; }
        
        #endregion
    }
}
