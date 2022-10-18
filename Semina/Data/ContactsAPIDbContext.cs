using Microsoft.EntityFrameworkCore;
using Semina.Models;

namespace Semina.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> contacts { get; set; }
    }
}
