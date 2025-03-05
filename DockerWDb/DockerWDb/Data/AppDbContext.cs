using DockerWDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerWDb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Usuarios { get; set; }
    }
}
