using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.AcademicQualification
{
    public class Delete
    {

        public class Model
        {
            public Guid Id { get; set; }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task Trash(Model model)
            {
                var aq = await db.AcademicQualifications.FindAsync(model.Id);

                if (aq == null) throw new Exception();

                db.AcademicQualifications.Remove(aq);

                await db.SaveChangesAsync();

            }


        }
    }
}
