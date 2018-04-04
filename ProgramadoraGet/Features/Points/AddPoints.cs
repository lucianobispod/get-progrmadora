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

            public async void SavePoints(Model model)
            {
                var user = await db.Users.SingleOrDefaultAsync(where => where.RFID == model.RFID);

                if (user == null) throw new HttpException(400, "Esse RFID não está atrelado a nenhum usuário");

                user.Points += 50;

               await db.SaveChangesAsync();
            }
        }

    }
}
