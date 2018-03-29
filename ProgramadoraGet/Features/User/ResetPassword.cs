using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
{
    public class ResetPassword
    {
        public class Model
        {
            public string Identifier { get; set; }

            public string NewPassword { get; set; }

            public string ConfirmedPassword { get; set; }
        }

        public class Services 
        {
            private readonly Db db;

            public Services(Db db)
            {
                this.db = db;
            }

            public async Task Handle(Model message)
            {
                if (!message.NewPassword.Equals(message.ConfirmedPassword)) throw new Exception();

                var decryptIdentifier = new Domain.RecoveryPassword().Decrypt(message.Identifier);

                var recovery = await db.RecoveryPasswords.FindAsync(decryptIdentifier);

                if (recovery == null) throw new ForbiddenException();
                if (recovery.AcessedAt != null) throw new NotFoundException();

                if (DateTime.Compare(recovery.CreatedAt, DateTime.Now) < 0) throw new Exception();

                var user = await db.Users.FindAsync(recovery.UserId);

                if (user.DeletedAt != null) throw new NotFoundException();

                user.SetPassword(message.ConfirmedPassword);

                await db.SaveChangesAsync();

            }
        }

    }
}
