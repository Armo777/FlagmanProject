using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using ServiceStack.Configuration;

namespace FlagmanProject.Models
{
    public class Block
    {

        //[Key]
        public Guid Id { get; set; }
        //public int Index { get; set; } // Индекс блока
        public DateTime Timestamp { get; set; } // Временная метка
        public string Name { get; set; } // Данные блока
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PreviousHash { get; set; } // Хэш предыдущего блока
        public string Hash { get; set; } // Хэш текущего блока
        public bool IsDeleted { get; set; }
        //public string salt { get; set; }
        // Конструктор блока
        public Block(/*DateTime now, */string name, string surname, string phone, string email, string previousHash/*, AppSettings appSettings*/ )
        {
            /*var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //var bb = configuration["AppSettings:salt"];
            var bb = configuration["AppSettings:salt"];*/
            //string Salt = appSettings.salt;
            //Timestamp = DateTime.Now;
            
            Name = CalculateName();
            Surname = CalculateSurname();
            Phone = CalculatePhone();
            Email = CalculateEmail();
            PreviousHash = previousHash;
            Hash = CalculateHash();
            IsDeleted = false;
            
        }

        // Метод для расчета хэша текущего блока
        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{PreviousHash}-{Name}-{Surname}-{Phone}-{Email}");            
            byte[] outputBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(outputBytes);
        }

        public string CalculateName()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //var bb = configuration["AppSettings:salt"];
            var bb = configuration["AppSettings:salt"];
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = Encoding.UTF8.GetBytes(bb);
                byte[] iv = new byte[16] { 0x32, 0x88, 0x31, 0xE0, 0x43, 0x5A, 0x31, 0x37, 0xF6, 0x30, 0x98, 0x07, 0xA8, 0x8D, 0xA2, 0x34 };

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(Name);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public string CalculateSurname()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //var bb = configuration["AppSettings:salt"];
            var bb = configuration["AppSettings:salt"];
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = Encoding.UTF8.GetBytes(bb);
                byte[] iv = new byte[16] { 0x32, 0x88, 0x31, 0xE0, 0x43, 0x5A, 0x31, 0x37, 0xF6, 0x30, 0x98, 0x07, 0xA8, 0x8D, 0xA2, 0x34 };

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(Surname);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public string CalculatePhone()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //var bb = configuration["AppSettings:salt"];
            var bb = configuration["AppSettings:salt"];
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = Encoding.UTF8.GetBytes(bb);
                byte[] iv = new byte[16] { 0x32, 0x88, 0x31, 0xE0, 0x43, 0x5A, 0x31, 0x37, 0xF6, 0x30, 0x98, 0x07, 0xA8, 0x8D, 0xA2, 0x34 };

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(Phone);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public string CalculateEmail()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //var bb = configuration["AppSettings:salt"];
            var bb = configuration["AppSettings:salt"];
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = Encoding.UTF8.GetBytes(bb);
                byte[] iv = new byte[16] { 0x32, 0x88, 0x31, 0xE0, 0x43, 0x5A, 0x31, 0x37, 0xF6, 0x30, 0x98, 0x07, 0xA8, 0x8D, 0xA2, 0x34 };

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(Email);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }
    }
}
