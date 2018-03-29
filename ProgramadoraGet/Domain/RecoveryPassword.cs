using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProgramadoraGet.Domain
{
    public class RecoveryPassword
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public DateTime? AcessedAt { get; set; }

        #region Navigation

        public virtual User User { get; set; }

        #endregion


        private byte[] key = Encoding.ASCII.GetBytes("sqpdlrai");
        private byte[] IV = Encoding.ASCII.GetBytes("senai132");

        private byte[] encrypted;


        public string Encrypt(string plainText)
        {
            try
            {
                using (DESCryptoServiceProvider crypto = new DESCryptoServiceProvider())
                {
                    crypto.Key = this.key;
                    crypto.IV = this.IV;

                    ICryptoTransform transform = crypto.CreateEncryptor(crypto.Key, crypto.IV);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(plainText);
                            }

                            encrypted = stream.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {

                { throw ex; }
            }
        }

        public Guid Decrypt(string cipherText)
        {
            try
            {
                string plainText = string.Empty;

                using (DESCryptoServiceProvider crypto = new DESCryptoServiceProvider())
                {
                    crypto.Key = this.key;
                    crypto.IV = this.IV;

                    ICryptoTransform transform = crypto.CreateDecryptor(crypto.Key, crypto.IV);

                    using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cryptoStream))
                            {
                                plainText = reader.ReadToEnd();
                            }
                        }
                    }
                }

                return Guid.Parse(plainText);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
