using ProgramadoraGet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Infrastructure
{
    public class DbInitializer
    {
        public static void Initialize(Db db)
        {
            db.Database.EnsureCreated();
            
            db.SaveChanges();
        }
    }
}
