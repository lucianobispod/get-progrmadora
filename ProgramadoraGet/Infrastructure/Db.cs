using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Infrastructure
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options) { }

        #region Tables

        #endregion

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);
                       
        }

    }
}
