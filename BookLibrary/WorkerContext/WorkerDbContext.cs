using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookLibrary.WorkerContext
{
    public class WorkerDbContext : DbContext
    {
        public WorkerDbContext(DbContextOptions<WorkerDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BookLibrary; User Id=sa; password=sqladmin; TrustServerCertificate= True");
        }

    }
}
