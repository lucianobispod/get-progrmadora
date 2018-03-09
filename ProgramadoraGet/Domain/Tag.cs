using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TagType TagType { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        #region Navigation

        public virtual ICollection<LikeTag> LikeTag { get; set; }

        public virtual ICollection<QuestionTag> QuestionTag { get; set; }

        public virtual ICollection<Skills> Skills { get; set; }

        #endregion

    }

    public enum TagType : int
    {
        Normal = 0,
        Technology = 1,
        Tool = 2,
    }
}
