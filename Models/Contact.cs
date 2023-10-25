using System.Collections.Generic;
using System.Security.Cryptography;

namespace FlagmanProject.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Phone { get; set; } 
        public string Email { get; set; }

    }
}
