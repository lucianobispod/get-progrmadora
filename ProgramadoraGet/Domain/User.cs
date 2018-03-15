using Microsoft.AspNetCore.Identity;
using ProgramadoraGet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class User 
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime? EmailVerifiedAt { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public  string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string Picture { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string State { get; set; }

        public string Location { get; set; }

        public bool IsPasswordEqualsTo(string password)
        {
            return Encrypt(password, this.PasswordSalt) == this.PasswordHash;
        }

        public void SetPassword(string password)
        {
            var salt = GenerateSalt();
            this.PasswordSalt = salt.ToBase64();
            this.PasswordHash = Encrypt(password, PasswordSalt);
        }

        private static string Encrypt(string password, string salt)
        {
            byte[] toEncrypt = salt.FromBase64().Concat(Encoding.UTF8.GetBytes(password)).ToArray();
            using (var sha = SHA512.Create()) return sha.ComputeHash(toEncrypt).ToBase64();
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[64];
            using (var generator = RandomNumberGenerator.Create()) generator.GetBytes(salt);
            return salt;
        }

        #region Navigation

        public virtual ICollection<Comment> Comment { get; set; }

        public virtual ICollection<Question> Question { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }

        public virtual ICollection<LikeTag> LikeTag { get; set; }

        public virtual ICollection<Match> Match { get; set; }

        public virtual ICollection<Notification> Notification { get; set; }

        public virtual ICollection<RecoveryPassword> RecoveryPassword { get; set; }

        public virtual ICollection<Skills> Skills { get; set; }

        public virtual ICollection<AcademicQualification> Historic { get; set; }

        #endregion


    }
}
