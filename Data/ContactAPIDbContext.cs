using FlagmanProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FlagmanProject.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public  DbSet<Block> Blocks { get; set; }
        //public DbSet<Contact> Contacts { get; set; }
    }
}
