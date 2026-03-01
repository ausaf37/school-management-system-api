using DempAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DempAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StudentEntity> StudentRegister { get; set; }

        public DbSet<LocalUsers> LocalUsers { get; set; }

        //protected ApplicationDbContext()
        //{
        //}
    }
}
