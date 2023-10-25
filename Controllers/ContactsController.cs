using FlagmanProject.Data;
using FlagmanProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FlagmanProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        /*private IConfiguration Config { get; }

        public ContactsController(IConfiguration config)
        {
            Config = config;
        }*/

        /*private string EncryptionKey;
        public ContactsController(string encryptionKey)
        {
           EncryptionKey = "YourEncryptionKey";
        }
        private string Encrypt(string data)
        {
           using (var aesAlg = new AesCryptoServiceProvider())
           {
               aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
               aesAlg.IV = new byte[16]; // Инициализируем вектор инициализации IV

               // Создаем шифратор
               var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

               using (var msEncrypt = new MemoryStream())
               {
                   using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                   {
                       using (var swEncrypt = new StreamWriter(csEncrypt))
                       {
                           // Записываем данные в шифрованный поток
                           swEncrypt.Write(data);
                       }
                   }

                   // Возвращаем зашифрованные данные в виде Base64 строки
                   return Convert.ToBase64String(msEncrypt.ToArray());
               }
           }
        }*/

        /*private string Decrypt(string encryptedData)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedData);

            using (var aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = new byte[16]; // Инициализируем вектор инициализации IV

                // Создаем расшифровщик
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Считываем расшифрованные данные из потока и возвращаем их как строку
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }*/

        private readonly ContactAPIDbContext dbContext;
        public ContactsController(ContactAPIDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlocks()
        {
            return Ok(await dbContext.Blocks.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddBlock([FromBody] AddBlockDto block)
        {
            var lastBlock = dbContext.Blocks.FirstOrDefault() ?? new Block("","","","","genesis");
            var newBlock = new Block(
                //lastBlock?.Index + 1 ?? 0,
                //DateTime.Now,
                block.Name,
                block.Surname,
                block.Phone,
                block.Email,
                lastBlock.Hash ?? "genesis"
            //appSettings, 
            //block.IsDeleted
            );
            dbContext.Blocks.Add(newBlock);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetBlocks), new { id = newBlock.Id }, newBlock);
        }

        /*[HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }*/

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Blocks.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        /*[HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Surname = addContactRequest.Surname,
                Phone = addContactRequest.Phone,
                Email = addContactRequest.Email,
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }*/

        /*[HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, /*UpdateContactRequest updateContactRequestBlock block)*/
        /*{
            var contact = await dbContext.Blocks.FindAsync(id);

            if (contact != null)
            {
                /*contact.Name = block.Name; //= updateContactRequest.Name;
                contact.Surname = block.Surname;
                contact.Phone = block.Phone;
                contact.Email = block.Email;*/

                /*dbContext.Remove(block);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }*/

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id, bool isSoftDelete)
        {
            var block = await dbContext.Blocks.FindAsync(id);

            if (block != null)
            {
                isSoftDelete = true;
                await dbContext.SaveChangesAsync();
                return Ok(block);
            }

            return NotFound();
        }
    }
}
