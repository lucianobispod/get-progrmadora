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

        public TagType Type { get; set; }

        public DateTime? DeletedAt { get; set; }


        #region Navigation

        public ICollection<LikeTag> LikeTag { get; set; }

        public ICollection<QuestionTag> QuestionTag { get; set; }

        public ICollection<Skills> Skills { get; set; }

        #endregion


        public enum TagType : int
        {
            Normal = 0,
            Technology = 1,
            Tool = 2,
        }
    }
}
