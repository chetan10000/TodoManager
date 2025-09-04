using Microsoft.EntityFrameworkCore;
using TodoManager.Models;
namespace TodoManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }

    }
    
}