using FluentValidation;
using Microsoft.Extensions.Configuration;
using ProgramadoraGet.Domain;
using ProgramadoraGet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProgramadoraGet.Features.User
{
    public class SendEmail
    {
        public class Model 
        {
            public string Email { get; set; }

        }

        public class Validator : AbstractValidator<Model>
        {
            public Validator()
            {
                RuleFor(validate => validate.Email).MaximumLength(100).NotEmpty();
            }
        }


        public class Result
        {
            public string Message { get; set; }
        }

        public class Services 
        {
            private readonly Db db;
            private readonly IConfiguration configuration;

            public Services(Db db, IConfiguration configuration)
            {
                this.db = db;
                this.configuration = configuration;
            }

            public async Task<Result> Send(Model model)
            {
                var user = db.Users.SingleOrDefault(s => s.Email == model.Email );

                if (user == null) throw new ForbiddenException();

                if (user.DeletedAt != null) throw new NotFoundException();


                var fromAddress = new MailAddress(configuration["Email:Email"]);
                var fromPassword = configuration["Email:Password"];

                var toAddress = new MailAddress(user.Email);

                var recordecovery = new RecoveryPassword
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.Now.AddMinutes(30),
                };

                db.RecoveryPasswords.Add(recordecovery);

                await db.SaveChangesAsync();


                string subject = "Recuperação de senha";

                var encripty = new RecoveryPassword().Encrypt(recordecovery.Id.ToString());

                string body = $"O link que você precisa acessar para redefimir a sua senha é http://getProgramadora.com.br/ResetPassword/{ encripty }";

                try
                {
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 465,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var email = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    await smtp.SendMailAsync(email);


                    return new Result { Message = "O Email para recuperação de senha foi enviado para " + user.Email };
                }
                catch (Exception ex)
                {
                    { throw ex; }
                }

            }



        }
    }
}
