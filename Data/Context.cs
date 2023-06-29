using Microsoft.EntityFrameworkCore;
using printingsystem.Models.Files;
using printingsystem.Models.Prints;
using printingsystem.Models.Users;

namespace printingsystem.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<Users> users { get; set; }
        public DbSet<Files> files { get; set; }
        public DbSet<Prints> prints { get; set; }
    }
}
