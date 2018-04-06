using Microsoft.EntityFrameworkCore;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.Points
{
    public class AddPoints
    {
        public class Model
        {
            public string RFID { get; set; }
        }

        public class Services
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task<string> SavePoints(Model model)
            {
                var user = await db.Users.FirstOrDefaultAsync(w => w.RFID.ToString().Equals(model.RFID.ToString()));

                if (user == null) throw new HttpException(400, "Esse RFID não está atrelado a nenhum usuário");

                user.Points += 50;

               await db.SaveChangesAsync();

                return "Pontos do usuário "+ user.Points;
            }
        }

    }
}
